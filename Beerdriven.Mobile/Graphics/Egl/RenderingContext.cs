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

namespace Beerdriven.Mobile.Graphics.Egl
{
    using System;
    using Enums;
    using Interop;
    using OpenGLESv2;

    public class RenderingContext : Disposable
    {
        private readonly IntPtr configPointer;

        private readonly ContextVersion clientVersion;

        private readonly IntPtr displayPointer;

        private bool isInitialized;

        internal RenderingContext(IntPtr displayPointer, IntPtr configPointer, ContextVersion clientVersion)
        {
            this.displayPointer = displayPointer;
            this.configPointer = configPointer;
            this.clientVersion = clientVersion;
            this.Initialize();
        }

        internal IntPtr ContextPointer
        {
            get;
            private set;
        }

        public void MakeCurrent(Surface draw, Surface read)
        {
            IntPtr drawPointer = IntPtr.Zero;
            IntPtr readPointer = IntPtr.Zero;

            if (draw != null)
            {
                drawPointer = draw.SurfacePointer;
            }

            if (read != null)
            {
                readPointer = read.SurfacePointer;
            }

            this.MakeCurrent(drawPointer, readPointer);
        }

        protected override void Dispose(bool disposing)
        {
            if (this.isInitialized)
            {
                NativeEgl.eglDestroyContext(this.displayPointer, this.ContextPointer);
            }

            base.Dispose(disposing);
        }

        protected void Initialize()
        {
            var contextAttribs = new Attribs<int>();
            contextAttribs.Add(NativeEgl.EGL_CONTEXT_CLIENT_VERSION, (int)this.clientVersion);
            contextAttribs.AddEnd();

            this.ContextPointer = NativeEgl.eglCreateContext(
                    this.displayPointer, this.configPointer, IntPtr.Zero, contextAttribs.ToIntArray());

            if (this.ContextPointer == IntPtr.Zero)
            {
                throw new PlatformGraphicsException("Failed to create context.", NativeEgl.eglGetError());
            }

            this.isInitialized = true;
        }

        protected void MakeCurrent(IntPtr draw, IntPtr read)
        {
            if (NativeEgl.eglMakeCurrent(this.displayPointer, draw, read, this.ContextPointer) == NativeEgl.EGL_FALSE)
            {
                throw new PlatformGraphicsException(
                        "Could not set rendering context to current.", NativeEgl.eglGetError());
            }
        }
    }
}