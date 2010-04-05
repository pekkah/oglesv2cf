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

    public class WindowSurface : Surface
    {
        private readonly IntPtr configPointer;

        private readonly IntPtr displayPointer;

        private bool isInitialized;

        internal WindowSurface(IntPtr windowHandle, IntPtr displayPointer, IntPtr configPointer)
        {
            this.displayPointer = displayPointer;
            this.configPointer = configPointer;
            this.WindowHandle = windowHandle;
            this.Initialize();
        }

        internal IntPtr WindowHandle
        {
            get;
            set;
        }

        public void SwapBuffers()
        {
            NativeEgl.eglSwapBuffers(this.displayPointer, this.SurfacePointer);
        }

        protected override void Dispose(bool disposing)
        {
            if (this.isInitialized)
            {
                if (NativeEgl.eglDestroySurface(this.displayPointer, this.SurfacePointer) == NativeEgl.EGL_FALSE)
                {
                    var errorCode = NativeEgl.eglGetError();
                    throw new PlatformGraphicsException("Could not destroy surface.", errorCode);
                }
            }

            base.Dispose(disposing);
        }

        protected void Initialize()
        {
            this.SurfacePointer = NativeEgl.eglCreateWindowSurface(
                    this.displayPointer, this.configPointer, this.WindowHandle, null);

            if (this.SurfacePointer == IntPtr.Zero)
            {
                var errorCode = NativeEgl.eglGetError();

                var errorMessage = string.Format(
                        "Could not create window surface. Error code {0}", errorCode.ToString("X"));
                throw new PlatformGraphicsException(errorMessage, errorCode);
            }

            this.isInitialized = true;
        }
    }
}