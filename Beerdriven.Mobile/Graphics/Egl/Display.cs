﻿#region license

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

    public class Display : Disposable
    {
        internal Display(IntPtr deviceContext)
        {
            this.DeviceContext = deviceContext;
            this.Initialize();
        }

        internal IntPtr DeviceContext
        {
            get;
            set;
        }

        internal IntPtr DisplayPointer
        {
            get;
            set;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // managed resources 
            }

            this.DeviceContext = IntPtr.Zero;
            this.DisplayPointer = IntPtr.Zero;

            base.Dispose(disposing);
        }

        protected void Initialize()
        {
            // get display
            this.DisplayPointer = NativeEgl.eglGetDisplay(this.DeviceContext);

            if (this.DisplayPointer == IntPtr.Zero)
            {
                throw new PlatformGraphicsException("Could not get display", NativeEgl.eglGetError());
            }
        }
    }
}