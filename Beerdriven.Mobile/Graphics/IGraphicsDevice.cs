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

namespace Beerdriven.Mobile.Graphics
{
    using System;
    using System.Drawing;
    using ES20;
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

        IRenderingScope Begin();

        void BindBuffer(DeviceBuffer buffer);

        void Clear(uint mask);

        DeviceBuffer CreateBuffer(uint target);

        void DisableBuffer(uint target);

        void DisableProgram();

        void DisableVertexAttribArray(uint indx);

        void DrawArrays(uint mode, int first, int count);

        void DrawElements(uint mode, int count, uint type);

        void EnableVertexAttribArray(uint indx);

        void UseProgram(ShaderProgram program);

        void VertexAttribPointer(uint indx, int size, uint type, byte normalized, int stride);

        void VertexAttribPointer(uint indx, int size, uint type, byte normalized, int stride, IntPtr ptr);
    }
}