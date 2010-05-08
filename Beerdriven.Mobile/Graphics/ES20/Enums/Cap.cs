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

namespace Beerdriven.Mobile.Graphics.ES20.Enums
{
    using Interop;

    public enum Cap : uint
    {
        /// <summary>
        ///   If enabled,
        ///   blend the computed fragment color values with the values in the color
        ///   buffers. See glBlendFunc.
        /// </summary>
        GL_BLEND = NativeGl.GL_BLEND,

        /// <summary>
        ///   If enabled,
        ///   cull polygons based on their winding in window coordinates.
        ///   See glCullFace.
        /// </summary>
        GL_CULL_FACE = NativeGl.GL_CULL_FACE,

        /// <summary>
        ///   If enabled,
        ///   do depth comparisons and update the depth buffer. Note that even if
        ///   the depth buffer exists and the depth mask is non-zero, the
        ///   depth buffer is not updated if the depth test is disabled. See
        ///   glDepthFunc and
        ///   glDepthRangef.
        /// </summary>
        GL_DEPTH_TEST = NativeGl.GL_DEPTH_TEST,

        /// <summary>
        ///   If enabled,
        ///   dither color components or indices before they are written to the
        ///   color buffer.
        /// </summary>
        GL_DITHER = NativeGl.GL_DITHER,

        /// <summary>
        ///   If enabled, an offset is added to depth values of a polygon's
        ///   fragments produced by rasterization.
        ///   See glPolygonOffset.
        /// </summary>
        GL_POLYGON_OFFSET_FILL = NativeGl.GL_POLYGON_OFFSET_FILL,

        /// <summary>
        ///   If enabled,
        ///   compute a temporary coverage value where each bit is determined by the
        ///   alpha value at the corresponding sample location.  The temporary coverage
        ///   value is then ANDed with the fragment coverage value.
        /// </summary>
        GL_SAMPLE_ALPHA_TO_COVERAGE = NativeGl.GL_SAMPLE_ALPHA_TO_COVERAGE,

        /// <summary>
        ///   If enabled,
        ///   the fragment's coverage is ANDed with the temporary coverage value.  If
        ///   GL_SAMPLE_COVERAGE_INVERT is set to GL_TRUE, invert the coverage
        ///   value.
        ///   See glSampleCoverage.
        /// </summary>
        GL_SAMPLE_COVERAGE = NativeGl.GL_SAMPLE_COVERAGE,

        /// <summary>
        ///   If enabled,
        ///   discard fragments that are outside the scissor rectangle.
        ///   See glScissor.
        /// </summary>
        GL_SCISSOR_TEST = NativeGl.GL_SCISSOR_TEST,

        /// <summary>
        ///   If enabled,
        ///   do stencil testing and update the stencil buffer.
        ///   See glStencilFunc and glStencilOp.
        /// </summary>
        GL_STENCIL_TEST = NativeGl.GL_STENCIL_TEST,
    }
}