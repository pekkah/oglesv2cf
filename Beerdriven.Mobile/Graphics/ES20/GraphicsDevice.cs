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
    using System.Drawing;
    using Interop;
    using OpenTK;

    public class GraphicsDevice : IGraphicsDevice
    {
        private Vector4 clearColor;

        private Rectangle viewport;

        public Vector4 ClearColor
        {
            get
            {
                return this.clearColor;
            }

            set
            {
                if (this.clearColor != value)
                {
                    this.UpdateClearColor(value);
                }

                this.clearColor = value;
            }
        }

        public Rectangle Viewport
        {
            get
            {
                return this.viewport;
            }

            set
            {
                this.UpdateViewport(value);
                this.viewport = value;
            }
        }

        public void Clear(uint mask)
        {
            NativeGl.glClear(mask);
        }

        public void DrawElements(uint mode, int count, uint type)
        {
            NativeGl.glDrawElements(mode, count, type, IntPtr.Zero);
        }

        public void EnabledVertexAttribArray(uint index)
        {
            NativeGl.glEnableVertexAttribArray(index);
        }

        public void VertexAttribPointer(uint indx, int size, uint type, byte normalized, int stride)
        {
            NativeGl.glVertexAttribPointer(indx, size, type, normalized, stride, IntPtr.Zero);
        }

        public IRenderingScope Begin()
        {
            return null;
        }

        public DeviceBuffer CreateBuffer(uint target)
        {
            return new DeviceBuffer(target);
        }

        public void BindBuffer(DeviceBuffer buffer)
        {
            NativeGl.glBindBuffer(buffer.Target, buffer.BufferId);
        }

        public void UseProgram(ShaderProgram program)
        {
            NativeGl.glUseProgram(program.ProgramId);
        }

        private void UpdateClearColor(Vector4 color)
        {
            NativeGl.glClearColor(color.X, color.Y, color.Z, color.W);
        }

        private void UpdateViewport(Rectangle vp)
        {
            NativeGl.glViewport(vp.Left, vp.Top, vp.Width, vp.Height);
        }
    }
}