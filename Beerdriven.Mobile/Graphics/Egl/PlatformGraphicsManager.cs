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
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Interop;
    using OpenGLESv2;

    /// <summary>
    ///   High level class implementing interface to Native Graphics Interface EGL.
    /// </summary>
    public class PlatformGraphicsManager : Disposable, IPlatformGraphicsManager
    {
        private readonly Display display;

        private bool isInitialized;

        public PlatformGraphicsManager(Display display)
        {
            this.display = display;
            this.Initialize();
        }

        public Version Version
        {
            get;
            protected set;
        }

        public void BindApi(uint api)
        {
            NativeEgl.eglBindAPI(api);
        }

        public IEnumerable<Config> ChooseConfigs(AttribList attribList, int numberOfConfigsToReturn)
        {
            var configs = new IntPtr[numberOfConfigsToReturn];

            int numberOfConfigsFound;
            if (NativeEgl.eglChooseConfig(
                    this.display.DisplayPointer,
                    attribList.ToIntArray(),
                    configs,
                    configs.Length,
                    out numberOfConfigsFound) == NativeEgl.EGL_FALSE)
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
            var renderingContext = new RenderingContext(this.display.DisplayPointer, config.ConfigPointer, attribList);

            return renderingContext;
        }

        public WindowSurface CreateWindowSurface(Config config, Form window)
        {
            return new WindowSurface(window.Handle, this.display.DisplayPointer, config.ConfigPointer);
        }

        public void Terminate()
        {
            this.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            if (this.isInitialized)
            {
                if (NativeEgl.eglTerminate(this.display.DisplayPointer) == NativeEgl.EGL_FALSE)
                {
                    throw new DeviceOperationException(
                            "Failed to terminate egl display connection", NativeEgl.eglGetError());
                }
            }

            base.Dispose(disposing);
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
    }
}