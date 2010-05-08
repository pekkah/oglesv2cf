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

using Beerdriven.Mobile.Graphics.OpenGLESv2;

namespace Beerdriven.Mobile.Graphics.ES20
{
    using System;
    using System.Runtime.InteropServices;
    using Enums;
    using Interop;
    using Mobile.Interop;
    using OpenGLESv2;

    public class Shader : Disposable
    {
        private const int MaxInfologLength = 2024;

        internal Shader(ShaderType type)
        {
            this.Type = type;
            this.Initialize();
        }

        public ShaderType Type
        {
            get;
            private set;
        }

        internal uint ShaderId
        {
            get;
            private set;
        }

        public bool Compile()
        {
            NativeGl.glCompileShader(this.ShaderId);

            var success = new[]
                              {
                                      -1
                              };

            NativeGl.glGetShaderiv(this.ShaderId, NativeGl.GL_COMPILE_STATUS, success);

            return success[0] == NativeGl.GL_TRUE;
        }

        public string GetInfoLog()
        {
            int length;
            var error = new char[MaxInfologLength];
            NativeGl.glGetShaderInfoLog(this.ShaderId, MaxInfologLength, out length, error);

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

                NativeGl.glShaderSource(this.ShaderId, 1, ptrArray, &length);
                Marshal.FreeHGlobal(strPtr);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (this.ShaderId != 0)
            {
                NativeGl.glDeleteShader(this.ShaderId);
            }

            base.Dispose(disposing);
        }

        protected void Initialize()
        {
            this.ShaderId = NativeGl.glCreateShader((uint)this.Type);
        }
    }
}