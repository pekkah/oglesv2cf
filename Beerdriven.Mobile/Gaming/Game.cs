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

namespace Beerdriven.Mobile.Gaming
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Forms;
    using Graphics.Egl;
    using Graphics.Egl.Interop;
    using Graphics.ES20.Interop;
    using Graphics.OpenGLESv2;
    using Interop;
    using OpenTK;

    public abstract class Game : Disposable
    {
        public Matrix4 Projection;

        public Matrix4 View;

        public Matrix4 World;

        protected bool ExitGame;

        private long counterFrequency;

        private long frameDeltaCount;

        private double frameDeltaSeconds;

        private long frameEndCount;

        private long frameStartCount;

        private NativeMessage message;

        protected esContext GraphicsContext
        {
            get;
            private set;
        }

        protected Form RenderingWindow
        {
            get;
            private set;
        }

        public void Run()
        {
            this.InitializeGraphics();
            this.LoadContent();
            this.RunApplication();
        }

        protected abstract void OnConfigureAttributes(eglAttribList attribs);

        protected abstract void OnLoadContent();

        protected abstract void OnRender(double deltaTime);

        protected abstract void OnUpdate(double deltaTime);

        protected virtual void OnHandleMessage(NativeMessage windowsMessage)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.GraphicsContext.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeGraphics()
        {
            try
            {
                this.GraphicsContext = new esContext(NativeEgl.EGL_OPENGL_ES_API);
                this.GraphicsContext.Initialize();

                var attribs = new eglAttribList();

                this.OnConfigureAttributes(attribs);

                var config = this.GraphicsContext.GetConfigs(attribs).FirstOrDefault();

                if (config == null)
                {
                    throw new InvalidOperationException("Could not find matching configuration");
                }

                this.GraphicsContext.OpenRenderingWindow(config);

                NativeGl.glClearColor(0, 0, 0, 1f);

                NativeGl.glViewport(0, 0, this.GraphicsContext.Width, this.GraphicsContext.Height);

                var error = NativeGl.glGetError();

                if (error != NativeGl.GL_NO_ERROR)
                {
                    throw new InvalidOperationException("Error while initializing graphics.");

                }
            }
            catch (eglException x)
            {
                MessageBox.Show(x.ToString());
                this.ExitGame = true;
            }
        }

        private void LoadContent()
        {
            try
            {
                this.OnLoadContent();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void Render(double deltaTime)
        {
            this.OnRender(deltaTime);

            this.GraphicsContext.SwapBuffers();

            var errorCode = NativeGl.glGetError();

            if (errorCode == NativeGl.GL_NO_ERROR)
            {
                return;
            }

            var codeString = errorCode.ToString("X");
            Trace.WriteLine(codeString);
            this.ExitGame = true;
        }

        private void RunApplication()
        {
            NativeCore.QueryPerformanceFrequency(ref this.counterFrequency);

            this.frameStartCount = this.frameEndCount = 0;

            while (!this.RunGameLoop())
            {
                Application.DoEvents();
            }

            Application.DoEvents();

            Application.Exit();
        }

        private bool RunGameLoop()
        {
            while (!NativeCore.PeekMessage(out this.message, IntPtr.Zero, 0, 0, 0))
            {
                this.frameDeltaCount = this.frameEndCount - this.frameStartCount;

                NativeCore.QueryPerformanceCounter(ref this.frameStartCount);
                this.frameDeltaSeconds = this.frameDeltaCount / (double)this.counterFrequency;

                this.Tick(this.frameDeltaSeconds);

                NativeCore.QueryPerformanceCounter(ref this.frameEndCount);
            }

            this.OnHandleMessage(this.message);

            return this.ExitGame;
        }

        private void Tick(double deltaTime)
        {
            this.Update(deltaTime);
            this.Render(deltaTime);
        }

        private void Update(double deltaTime)
        {
            this.OnUpdate(deltaTime);
        }
    }
}