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
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using Interop;

    public class TextureFactory
    {
        private readonly IGraphicsDevice device;

        public TextureFactory(IGraphicsDevice device)
        {
            if (device == null)
            {
                throw new ArgumentNullException("device");
            }

            this.device = device;
        }

        public Texture CreateFromBytes(byte[] data, int width, int height, uint format, uint type)
        {
            uint textureId;

            uint[] tex = new uint[1];

            NativeGl.glGenTextures(1, tex);

            textureId = tex[0];

            NativeGl.glActiveTexture(NativeGl.GL_TEXTURE0);
            NativeGl.glBindTexture(NativeGl.GL_TEXTURE_2D, textureId);

            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                NativeGl.glTexParameterf(NativeGl.GL_TEXTURE_2D, NativeGl.GL_TEXTURE_MIN_FILTER, NativeGl.GL_LINEAR);
                NativeGl.glTexParameterf(NativeGl.GL_TEXTURE_2D, NativeGl.GL_TEXTURE_MAG_FILTER, NativeGl.GL_LINEAR);
                NativeGl.glTexParameterf(NativeGl.GL_TEXTURE_2D, NativeGl.GL_TEXTURE_WRAP_S, NativeGl.GL_CLAMP_TO_EDGE);
                NativeGl.glTexParameterf(NativeGl.GL_TEXTURE_2D, NativeGl.GL_TEXTURE_WRAP_T, NativeGl.GL_CLAMP_TO_EDGE);

                NativeGl.glTexImage2D(
                        NativeGl.GL_TEXTURE_2D,
                        0,
                        (int)format,
                        width,
                        height,
                        0,
                        format,
                        type,
                        handle.AddrOfPinnedObject());
            }
            catch (Exception x)
            {
                NativeGl.glDeleteTextures(1, tex);
                throw;
            }
            finally
            {
                handle.Free();
            }

            NativeGl.glBindTexture(NativeGl.GL_TEXTURE_2D, 0);

            var texture = new Texture(NativeGl.GL_TEXTURE_2D, textureId);
            return texture;
        }

        public Texture CreateFromFile(string filename)
        {
            using (var fileStream = new FileStream(filename, FileMode.Open))
            {
                int width, height;
                var data = this.LoadBitmap(fileStream, out width, out height);
                return this.CreateFromBytes(data, width, height, NativeGl.GL_RGB, NativeGl.GL_UNSIGNED_SHORT_5_6_5);
            }
        }

        private byte[] LoadBitmap(FileStream fileStream, out int width, out int height)
        {
            var bitmap = new Bitmap(fileStream);

            var bitmapData = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format16bppRgb565);

            int numBytes = bitmapData.Stride * bitmapData.Height;

            var bytes = new byte[numBytes];
            Marshal.Copy(bitmapData.Scan0, bytes, 0, numBytes);

            bitmap.UnlockBits(bitmapData);
            bitmap.Dispose();

            width = bitmapData.Width;
            height = bitmapData.Height;
            return bytes;
        }
    }
}