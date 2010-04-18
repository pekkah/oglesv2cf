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
    using System.Collections.Generic;
    using OpenGLESv2;

    public class RenderingScope : Disposable, IRenderingScope
    {
        public readonly IList<uint> enabledBufferTargets;

        private readonly IGraphicsDevice device;

        private readonly IList<uint> enabledVertexAttribArrays;

        private bool enabledProgram;

        public RenderingScope(IGraphicsDevice device)
        {
            if (device == null)
            {
                throw new ArgumentNullException("device");
            }

            this.device = device;

            // state lists
            this.enabledVertexAttribArrays = new List<uint>(10);
            this.enabledBufferTargets = new List<uint>(2);
        }

        public void BindBuffer(DeviceBuffer buffer)
        {
            this.TrackBoundBuffer(buffer);
            this.device.BindBuffer(buffer);
        }

        public void EnableVertexAttribArray(uint index)
        {
            this.TrackEnableVertexAttribArray(index);
            this.device.EnableVertexAttribArray(index);
        }

        public void UseProgram(ShaderProgram program)
        {
            this.enabledProgram = true;
            this.device.UseProgram(program);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DisableVertexAttribArrays();

                if (this.enabledProgram)
                {
                    this.device.DisableProgram();
                    this.enabledProgram = false;
                }

                this.DisableBufferTargets();
            }

            base.Dispose(disposing);
        }

        private void DisableBufferTargets()
        {
            foreach (var target in this.enabledBufferTargets)
            {
                this.device.DisableBuffer(target);
            }

            this.enabledBufferTargets.Clear();
        }

        private void DisableVertexAttribArrays()
        {
            foreach (var vaId in this.enabledVertexAttribArrays)
            {
                this.device.DisableVertexAttribArray(vaId);
            }

            this.enabledVertexAttribArrays.Clear();
        }

        private void TrackBoundBuffer(DeviceBuffer buffer)
        {
            this.enabledBufferTargets.Add(buffer.Target);
        }

        private void TrackEnableVertexAttribArray(uint index)
        {
            this.enabledVertexAttribArrays.Add(index);
        }
    }
}