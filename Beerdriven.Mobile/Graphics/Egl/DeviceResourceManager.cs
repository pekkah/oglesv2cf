namespace Beerdriven.Mobile.Graphics.Egl
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Beerdriven.Mobile.Graphics.Egl.Interop;
    using Beerdriven.Mobile.Graphics.OpenGLESv2;

    /// <summary>
    /// High level class implementing interface to Native Graphics Interface EGL.
    /// </summary>
    public class DeviceResourceManager : Disposable, IDeviceResourceManager
    {
        private readonly Display display;

        private bool isInitialized;

        public DeviceResourceManager(Display display)
        {
            this.display = display;
            this.Initialize();
        }

        public Version Version
        {
            get;
            protected set;
        }

        protected void Initialize()
        {
            int major, minor;

            if (NativeEgl.eglInitialize(this.display.DisplayPointer, out major, out minor) == NativeEgl.EGL_FALSE)
            {
                var errorCode = NativeEgl.eglGetError();
                throw new DeviceOperationException("Could not intitialize egl display connection.", errorCode);
            }

            this.Version = new Version(major, minor);
            this.isInitialized = true;
        }

        public WindowSurface CreateWindowSurface(Config config, Form window)
        {
            return new WindowSurface(window.Handle, this.display.DisplayPointer, config.ConfigPointer);
        }

        public void Terminate()
        {
            this.Dispose();
        }

        public IEnumerable<Config> ChooseConfigs(AttribList attribList, int numberOfConfigsToReturn)
        {
            var configs = new IntPtr[numberOfConfigsToReturn];

            int numberOfConfigsFound;
            if (NativeEgl.eglChooseConfig(this.display.DisplayPointer, attribList.ToIntArray(), configs, configs.Length, out numberOfConfigsFound)
                == NativeEgl.EGL_FALSE)
            {
                throw new DeviceOperationException("Could not choose configurations.", NativeEgl.eglGetError());
            }

            var result = new List<Config>();

            for (int i = 0; i < numberOfConfigsFound; i++)
            {
                result.Add(new Config(configs[i]));
            }

            return result;
        }

        public RenderingContext CreateContext(Config config, AttribList attribList)
        {
            var renderingContext = new RenderingContext(this.display.DisplayPointer,config.ConfigPointer, attribList);

            return renderingContext;
        }

        public void BindApi(uint api)
        {
            NativeEgl.eglBindAPI(api);
        }

        protected override void Dispose(bool disposing)
        {
            if (this.isInitialized)
            {
                if (NativeEgl.eglTerminate(this.display.DisplayPointer) == NativeEgl.EGL_FALSE)
                {
                    throw new DeviceOperationException("Failed to terminate egl display connection", NativeEgl.eglGetError());
                }
            }

            base.Dispose(disposing);
        }
    }
}
