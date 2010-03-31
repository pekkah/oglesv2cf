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
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Interop;
    using Mobile.Interop;
    using OpenGLESv2;

    public class esContext : Disposable
    {
        public esContext(uint api)
        {
            NativeEgl.eglBindAPI(api);
        }

        public int Height
        {
            get
            {
                return this.DisplayWindow.Height;
            }
        }

        public int Width
        {
            get
            {
                return this.DisplayWindow.Width;
            }
        }

        protected eglContext CurrentContext
        {
            get;
            private set;
        }

        protected eglDisplay Display
        {
            get;
            private set;
        }

        protected RenderingWindow DisplayWindow
        {
            get;
            private set;
        }

        protected eglSurface WindowSurface
        {
            get;
            private set;
        }

        public IEnumerable<eglConfig> GetConfigs()
        {
            // get configurations, max 30
            int numConfigs;
            var configs = new IntPtr[30];

            if (NativeEgl.eglGetConfigs(this.Display.Display, out configs, configs.Length, out numConfigs)
                == NativeEgl.EGL_FALSE)
            {
                throw new eglException("Could not get configurations.", NativeEgl.eglGetError());
            }

            return this.GetConfigs(configs, numConfigs);
        }

        public IEnumerable<eglConfig> GetConfigs(eglAttribList attribList)
        {
            // get configurations, max 30
            int numConfigs;
            var configs = new IntPtr[10];

            if (NativeEgl.eglChooseConfig(
                    this.Display.Display, attribList.ToIntArray(), configs, configs.Length, out numConfigs)
                == NativeEgl.EGL_FALSE)
            {
                throw new eglException("Could not choose configurations.", NativeEgl.eglGetError());
            }

            return this.GetConfigs(configs, numConfigs);
        }

        public void Initialize()
        {
            this.DisplayWindow = new RenderingWindow
                                     {
                                             WindowState = FormWindowState.Maximized
                                     };

            this.Display = new eglDisplay(NativeCore.GetDC(this.DisplayWindow.Handle));
            this.Display.Initialize();
        }

        public void OpenRenderingWindow(eglConfig config)
        {
            this.WindowSurface = eglWindowSurface.Create(this.Display, config, this.DisplayWindow);

            this.CurrentContext = eglContext.Create(this.Display, config, null, null);

            this.CurrentContext.MakeCurrent(this.WindowSurface, this.WindowSurface);

            this.DisplayWindow.Show();
        }

        public void SwapBuffers()
        {
            if (NativeEgl.eglSwapBuffers(this.Display.Display, this.WindowSurface.Surface) == NativeEgl.EGL_FALSE)
            {
                throw new eglException("Failed to swap buffers.", NativeEgl.eglGetError());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Display.Dispose();
                this.DisplayWindow.Dispose();
            }

            base.Dispose(disposing);
        }

        protected IEnumerable<eglConfig> GetConfigs(IntPtr[] configs, int numOfConfigs)
        {
            var result = new List<eglConfig>();

            for (int i = 0; i < numOfConfigs; i++)
            {
                result.Add(new eglConfig(configs[i]));
            }

            return result;
        }
    }
}