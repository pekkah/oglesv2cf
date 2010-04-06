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

namespace Beerdriven.Mobile.Graphics.ES20
{
    using System;
    using System.Runtime.InteropServices;
    using Interop;
    using OpenGLESv2;

    public class DeviceBuffer : Disposable
    {
        private uint buffer;

        internal uint BufferId
        {
            get;
            private set;
        }

        internal uint Target
        {
            get;
            private set;
        }

        internal DeviceBuffer(uint target)
        {
            this.Target = target;
            this.Initialize();
        }

        public void BufferData<T>(int size, [In] T[] data, uint usage) where T : struct
        {
            GCHandle dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);

            try
            {
                NativeGl.glBufferData(this.Target, (IntPtr)size, dataHandle.AddrOfPinnedObject(), usage);
            }
            finally
            {
                dataHandle.Free();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (this.buffer > 0)
            {
                NativeGl.glDeleteBuffers(
                        1,
                        new[]
                            {
                                    this.buffer
                            });
            }

            base.Dispose(disposing);
        }

        protected void Initialize()
        {
            uint[] bufferIds = new uint[1];
            NativeGl.glGenBuffers(1, bufferIds);

            this.BufferId = bufferIds[0];
        }
    }
}