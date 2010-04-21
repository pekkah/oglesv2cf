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
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using Graphics;
    using Graphics.Egl;
    using Graphics.Egl.Enums;
    using Graphics.Egl.Interop;
    using Graphics.ES20;
    using Graphics.ES20.Interop;
    using Graphics.OpenGLESv2;
    using Interop;
    using OpenTK;

    public abstract class Game : Disposable
    {
        public Matrix4 Projection;

        // public Matrix4 View;

        public Matrix4 World;

        protected bool ExitGame;

        private long counterFrequency;

        private double requestedUpdateDelta = 1d / 30d;

        private long updateDeltaCount;

        private double updateDeltaSeconds;

        private long updateEndCount;

        private long updateStartCount;

        private double requestedRenderDelta = 1d / 30d;

        private long renderDeltaCount;

        private double renderDeltaSeconds;

        private long renderEndCount;

        private long renderStartCount;

        private NativeMessage message;

        protected IDisplayManager DisplayManager
        {
            get;
            private set;
        }

        protected WindowSurface RenderingSurface
        {
            get;
            private set;
        }

        protected RenderingContext RenderingContext
        {
            get;
            private set;
        }

        protected IPlatformGraphicsManager PlatformManager
        {
            get;
            private set;
        }

        protected IGraphicsDevice GraphicsDevice
        {
            get;
            private set;
        }

        protected RenderingWindow RenderingWindow
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

        protected abstract void OnConfigureAttributes(Attribs<ConfigAttributes> attribs);

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
                this.RenderingContext.Dispose();
                this.RenderingSurface.Destroy();
                this.PlatformManager.Terminate();
            }

            base.Dispose(disposing);
        }

        private void InitializeGraphics()
        {
            try
            {
                // platform graphics setup
                this.RenderingWindow = new RenderingWindow();
                this.RenderingWindow.Show();

                this.DisplayManager = new DisplayManager();

                this.PlatformManager = new PlatformGraphicsManager(this.DisplayManager.GetDisplay(this.RenderingWindow));

                this.PlatformManager.BindApi(Api.EGL_OPENGL_ES_API);

                var attribs = new Attribs<ConfigAttributes>();

                this.OnConfigureAttributes(attribs);

                var config = this.PlatformManager.ChooseConfigs(attribs, 1).FirstOrDefault();

                if (config == null)
                {
                    throw new InvalidOperationException("Could not find matching configuration");
                }

                this.RenderingContext = this.PlatformManager.CreateContext(config, ContextVersion.OPENGL_ES_2);

                this.RenderingSurface = this.PlatformManager.CreateWindowSurface(config, this.RenderingWindow);

                this.RenderingContext.MakeCurrent(this.RenderingSurface, this.RenderingSurface);

                // graphics device setup
                this.GraphicsDevice = new GraphicsDevice
                                          {
                                                  ClearColor = new Vector4(1, 1, 1, 1f),
                                                  Viewport = new Rectangle(
                                                      0, 
                                                      0, 
                                                      this.RenderingWindow.Width, 
                                                      this.RenderingWindow.Height)
                                          };

                var error = NativeGl.glGetError();

                if (error != NativeGl.GL_NO_ERROR)
                {
                    throw new InvalidOperationException("Error while initializing graphics.");

                }
            }
            catch (PlatformGraphicsException x)
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

            this.RenderingSurface.SwapBuffers();

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

            NativeCore.QueryPerformanceCounter(ref this.updateStartCount);

            this.updateEndCount = this.updateStartCount;

            this.renderStartCount = this.updateEndCount;
            this.renderEndCount = this.renderStartCount;

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
                NativeCore.QueryPerformanceCounter(ref this.updateEndCount);
                this.updateDeltaCount = this.updateEndCount - this.updateStartCount;
                this.updateDeltaSeconds = this.updateDeltaCount / (double)this.counterFrequency;

                if (this.updateDeltaSeconds >= this.requestedUpdateDelta)
                {
                    this.Update(this.updateDeltaSeconds);
                    NativeCore.QueryPerformanceCounter(ref this.updateStartCount);
                }

                NativeCore.QueryPerformanceCounter(ref this.renderEndCount);
                this.renderDeltaCount = this.renderEndCount - this.renderStartCount;
                this.renderDeltaSeconds = this.renderDeltaCount / (double)this.counterFrequency;

                if (this.renderDeltaSeconds >= this.requestedRenderDelta)
                {
                    this.Render(this.renderDeltaSeconds);
                    NativeCore.QueryPerformanceCounter(ref this.renderStartCount);
                }
            }

            this.OnHandleMessage(this.message);

            return this.ExitGame;
        }

        private void Update(double deltaTime)
        {
            this.OnUpdate(deltaTime);
        }
    }
}