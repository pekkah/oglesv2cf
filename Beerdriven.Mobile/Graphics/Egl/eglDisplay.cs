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

    public class eglDisplay : Disposable
    {
        private bool isInitialized;


        internal eglDisplay(IntPtr deviceContext)
        {
            this.DeviceContext = deviceContext;
        }

        internal IntPtr DeviceContext
        {
            get;
            set;
        }

        internal IntPtr Display
        {
            get;
            set;
        }

        public Version Initialize()
        {
            // get display
            this.Display = NativeEgl.eglGetDisplay(this.DeviceContext);

            if (this.Display == IntPtr.Zero)
            {
                throw new eglException("Could not get display", NativeEgl.eglGetError());
            }

            // initialize display connection
            int major, minor;
            if (NativeEgl.eglInitialize(this.Display, out major, out minor) == NativeEgl.EGL_FALSE)
            {
                var errorCode = NativeEgl.eglGetError();

                throw new eglException("Could not intitialize egl display connection.", errorCode);
            }

            this.isInitialized = true;

            return new Version(major, minor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // managed resources 
            }

            if (this.isInitialized)
            {
                if (NativeEgl.eglTerminate(this.Display) == NativeEgl.EGL_FALSE)
                {
                    throw new eglException("Failed to terminate egl display connection", NativeEgl.eglGetError());
                }
            }

            this.DeviceContext = IntPtr.Zero;
            this.Display = IntPtr.Zero;

            base.Dispose(disposing);
        }
    }
}