namespace Beerdriven.Mobile.Graphics
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using ES20.Interop;
    using OpenTK;

    public interface IGraphicsDevice
    {
        Vector4 ClearColor
        {
            get;

            set;
        }

        Rectangle Viewport
        {
            get;

            set;
        }

        void VertexAttribPointer(uint indx, int size, uint type, byte normalized, int stride);

        void EnabledVertexAttribArray(uint indx);

        void Clear(uint mask);

        void DrawElements(uint mode, int count, uint type);
    }

    public class GraphicsDevice : IGraphicsDevice
    {
        private Vector4 clearColor;

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

        private Rectangle viewport;

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

        public void EnabledVertexAttribArray(uint index)
        {
            NativeGl.glEnableVertexAttribArray(index);
        }

        public void Clear(uint mask)
        {
            NativeGl.glClear(mask);
        }

        public void DrawElements(uint mode, int count, uint type)
        {
            NativeGl.glDrawElements(mode, count, type, IntPtr.Zero);
        }

        public void VertexAttribPointer(uint indx, int size, uint type, byte normalized, int stride)
        {
            NativeGl.glVertexAttribPointer(
                        indx,
                        size,
                        type,
                        normalized,
                        stride,
                        IntPtr.Zero);

        }

        private void UpdateViewport(Rectangle vp)
        {
            NativeGl.glViewport(vp.Left, vp.Top, vp.Width, vp.Height);
        }

        private void UpdateClearColor(Vector4 color)
        {
            NativeGl.glClearColor(color.X, color.Y, color.Z, color.W);
        }
    }
}
