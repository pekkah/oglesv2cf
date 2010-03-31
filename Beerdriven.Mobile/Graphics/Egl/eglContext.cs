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

    public class eglContext : Disposable
    {
        internal eglContext(IntPtr context, IntPtr display)
        {
            this.Context = context;
            this.Display = display;
        }

        internal IntPtr Context
        {
            get;
            private set;
        }

        private IntPtr Display
        {
            get;
            set;
        }

        public static eglContext Create(
                eglDisplay display, eglConfig config, eglContext shareContext, eglAttribList attribList)
        {
            int[] attribs = null;
            IntPtr share_context = IntPtr.Zero;

            if (attribList != null)
            {
                attribs = attribList.ToIntArray();
            }

            if (shareContext != null)
            {
                share_context = shareContext.Context;
            }

            var context = NativeEgl.eglCreateContext(display.Display, config.Config, share_context, attribs);

            if (context == IntPtr.Zero)
            {
                throw new eglException("Failed to create context.", NativeEgl.eglGetError());
            }

            return new eglContext(context, display.Display);
        }

        public void MakeCurrent(eglSurface draw, eglSurface read)
        {
            NativeEgl.eglMakeCurrent(this.Display, draw.Surface, read.Surface, this.Context);
        }

        protected override void Dispose(bool disposing)
        {
            // release current context          
            NativeEgl.eglMakeCurrent(this.Display, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

            this.Context = IntPtr.Zero;
            this.Display = IntPtr.Zero;

            base.Dispose(disposing);
        }
    }
}