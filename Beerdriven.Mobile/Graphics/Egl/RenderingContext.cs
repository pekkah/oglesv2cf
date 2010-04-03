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
    using Interop;
    using OpenGLESv2;

    public class RenderingContext : Disposable
    {
        private readonly AttribList attribList;

        private readonly IntPtr configPointer;

        private readonly IntPtr displayPointer;

        private Surface drawSurface;

        private bool isInitialized;

        private Surface readSurface;

        internal RenderingContext(IntPtr displayPointer, IntPtr configPointer, AttribList attribList)
        {
            this.displayPointer = displayPointer;
            this.configPointer = configPointer;
            this.attribList = attribList;
            this.Initialize();
        }

        internal IntPtr ContextPointer
        {
            get;
            private set;
        }

        public void MakeCurrent(Surface draw, Surface read)
        {
            this.drawSurface = draw;
            this.readSurface = read;

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

        public void SwapBuffers()
        {
            NativeEgl.eglSwapBuffers(this.displayPointer, this.drawSurface.SurfacePointer);
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
            this.ContextPointer = NativeEgl.eglCreateContext(
                    this.displayPointer, this.configPointer, IntPtr.Zero, this.attribList.ToIntArray());

            if (this.ContextPointer == IntPtr.Zero)
            {
                throw new DeviceOperationException("Failed to create context.", NativeEgl.eglGetError());
            }

            this.isInitialized = true;
        }

        protected void MakeCurrent(IntPtr draw, IntPtr read)
        {
            if (NativeEgl.eglMakeCurrent(this.displayPointer, draw, read, this.ContextPointer) == NativeEgl.EGL_FALSE)
            {
                throw new DeviceOperationException(
                        "Could not set rendering context to current.", NativeEgl.eglGetError());
            }
        }
    }
}