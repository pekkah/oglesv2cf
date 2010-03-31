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
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using Interop;
    using Mobile.Interop;
    using OpenGLESv2;

    public class glShaderProgram : Disposable
    {
        private const int MaxInfologLength = 2024;

        private readonly List<glShader> shaders;

        public glShaderProgram()
        {
            this.Create();
            this.shaders = new List<glShader>();
        }

        public IEnumerable<glShader> Shaders
        {
            get
            {
                return this.shaders;
            }
        }

        protected uint Program
        {
            get;
            private set;
        }

        public void AttachShader(glShader shader)
        {
            NativeGl.glAttachShader(this.Program, shader.Shader);
            this.shaders.Add(shader);
        }

        public uint GetAttribLocation(string name)
        {
            var pname = MarshalExtensions.StringToPtrAnsi(name);
            var handle = NativeGl.glGetAttribLocation(this.Program, pname);
            Marshal.FreeHGlobal(pname);

            return handle;
        }

        public string GetInfoLog()
        {
            int length;
            var error = new char[MaxInfologLength];
            NativeGl.glGetProgramInfoLog(this.Program, MaxInfologLength, out length, error);

            return StringExtensions.GetAnsiString(error, length);
        }

        public uint GetUniformLocation(string name)
        {
            var pname = MarshalExtensions.StringToPtrAnsi(name);
            var handle = NativeGl.glGetUniformLocation(this.Program, pname);
            Marshal.FreeHGlobal(pname);

            return handle;
        }

        public bool Link()
        {
            this.OnBeforeLinked();

            NativeGl.glLinkProgram(this.Program);

            var success = new[]
                              {
                                      -1
                              };

            NativeGl.glGetProgramiv(this.Program, NativeGl.GL_LINK_STATUS, success);

            var ok = success[0] == NativeGl.GL_TRUE;

            if (ok)
            {
                this.OnLinked();
            }

            return ok;
        }

        public void Quit()
        {
            NativeGl.glUseProgram(0);
        }

        public void Use()
        {
            NativeGl.glUseProgram(this.Program);
        }

        protected virtual void OnBeforeLinked()
        {
        }

        protected virtual void OnLinked()
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var shader in this.shaders)
                {
                    NativeGl.glDetachShader(this.Program, shader.Shader);
                    shader.Dispose();
                }
            }

            if (this.Program != 0)
            {
                NativeGl.glDeleteProgram(this.Program);
            }

            base.Dispose(disposing);
        }

        private void Create()
        {
            this.Program = NativeGl.glCreateProgram();
        }
    }
}