#region license

// Copyright (c) 2010 Pekka Heikura
// 
//  Permission is hereby granted, free of charge, to any person
//  obtaining a copy of this software and associated documentation
//  files (the "Software"), to deal in the Software without
//  restriction, including without limitation the rights to use,
//  copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the
//  Software is furnished to do so, subject to the following
//  conditions:
// 
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
// 
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//  OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//  WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//  OTHER DEALINGS IN THE SOFTWARE.
// 

#endregion

namespace Beerdriven.Mobile.Graphics.ES20
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using Interop;
    using Mobile.Interop;
    using OpenGLESv2;

    public class glShader : Disposable
    {
        private const int MaxInfologLength = 2024;

        public glShader(uint type)
        {
            this.Type = type;
            this.Create();
        }

        public uint Type
        {
            get;
            private set;
        }

        internal uint Shader
        {
            get;
            private set;
        }

        public static glShader CompileFromFile(string fileName, uint type)
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentException("File does not exist");
            }

            glShader shader = new glShader(type);

            var source = string.Empty;

            using (var reader = File.OpenText(fileName))
            {
                source = reader.ReadToEnd();
            }

            shader.ShaderSource(source);

            if (!shader.Compile())
            {
                var infoLog = shader.GetInfoLog();

                shader.Dispose();

                throw new InvalidOperationException(string.Format("Failed to compile shader.\n {0}", infoLog));
            }

            return shader;
        }

        public bool Compile()
        {
            NativeGl.glCompileShader(this.Shader);

            var success = new[]
                              {
                                      -1
                              };

            NativeGl.glGetShaderiv(this.Shader, NativeGl.GL_COMPILE_STATUS, success);

            return success[0] == NativeGl.GL_TRUE;
        }

        public string GetInfoLog()
        {
            int length;
            var error = new char[MaxInfologLength];
            NativeGl.glGetShaderInfoLog(this.Shader, MaxInfologLength, out length, error);

            return StringExtensions.GetAnsiString(error, length);
        }

        public void ShaderSource(string source)
        {
            unsafe
            {
                int length = source.Length;

                IntPtr[] ptrArray = new IntPtr[1];
                IntPtr strPtr = MarshalExtensions.StringToPtrAnsi(source);
                ptrArray[0] = strPtr;

                NativeGl.glShaderSource(this.Shader, 1, ptrArray, &length);
                Marshal.FreeHGlobal(strPtr);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (this.Shader != 0)
            {
                NativeGl.glDeleteShader(this.Shader);
            }

            base.Dispose(disposing);
        }

        private void Create()
        {
            this.Shader = NativeGl.glCreateShader(this.Type);
        }
    }
}