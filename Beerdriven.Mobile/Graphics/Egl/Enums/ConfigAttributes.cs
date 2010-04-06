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

namespace Beerdriven.Mobile.Graphics.Egl.Enums
{
    using Interop;

    public enum ConfigAttributes
    {
        /// <summary>
        ///   Must be followed by a nonnegative integer that indicates the desired color
        ///   buffer size. The smallest color buffer of at least the specified size is 
        ///   preferred. 
        /// 
        ///   <para>
        ///     The default value is 0.
        ///   </para>
        /// </summary>
        EGL_BUFFER_SIZE = NativeEgl.EGL_BUFFER_SIZE,

        /// <summary>
        ///   Must be followed by a nonnegative minimum size specification. If this value 
        ///   is zero, the smallest available red buffer is preferred. Otherwise, the 
        ///   largest available red buffer of at least the minimum size is preferred. 
        /// 
        ///   <para>
        ///     The default value is 0.
        ///   </para>
        /// </summary>
        EGL_RED_SIZE = NativeEgl.EGL_RED_SIZE,

        /// <summary>
        ///   Must be followed by a nonnegative minimum size specification. 
        ///   If this value is zero, the smallest available green buffer is preferred. 
        ///   Otherwise, the largest available green buffer of at least the minimum size 
        ///   is preferred. 
        /// 
        ///   <para>
        ///     The default value is 0.
        ///   </para>
        /// </summary>
        EGL_GREEN_SIZE = NativeEgl.EGL_GREEN_SIZE,

        /// <summary>
        ///   Must be followed by a nonnegative minimum size specification. 
        ///   If this value is zero, the smallest available blue buffer is preferred. 
        ///   Otherwise, the largest available blue buffer of at least the minimum size 
        ///   is preferred. 
        /// 
        ///   <para>
        ///     The default value is 0.
        ///   </para>
        /// </summary>
        EGL_BLUE_SIZE = NativeEgl.EGL_BLUE_SIZE,

        /// <summary>
        ///   Must be followed by a nonnegative minimum size specification. 
        ///   If this value is zero, the smallest available alpha buffer is preferred. 
        ///   Otherwise, the largest available alpha buffer of at least the minimum size 
        ///   is preferred. 
        /// 
        ///   <para>
        ///     The default value is 0.
        ///   </para>
        /// </summary>
        EGL_ALPHA_SIZE = NativeEgl.EGL_ALPHA_SIZE,

        ///<summary>
        ///  Must be followed by one of EGL_DONT_CARE, EGL_NONE, EGL_SLOW_CONFIG, 
        ///  EGL_NON_CONFORMANT_CONFIG. If EGL_NONE is specified, then only frame 
        ///  buffer configurations with no caveats will be considered. 
        /// 
        ///  If EGL_SLOW_CONFIG is specified, then only slow frame buffer configurations 
        ///  will be considered. If EGL_NON_CONFORMANT_CONFIG is specified, then only 
        ///  non-conformant frame buffer configurations will be considered. 
        ///
        ///  <para>
        ///    The default value is EGL_DONT_CARE.
        ///  </para>
        ///</summary>
        EGL_CONFIG_CAVEAT = NativeEgl.EGL_CONFIG_CAVEAT,

        /// <summary>
        ///   Must be followed by a valid ID that indicates the desired EGL frame buffer 
        ///   configuration. When a EGL_CONFIG_ID is specified, all attributes are ignored. 
        /// 
        ///   <para>
        ///     The default value is EGL_DONT_CARE.
        ///   </para>
        /// </summary>
        EGL_CONFIG_ID = NativeEgl.EGL_CONFIG_ID,

        /// <summary>
        ///   Must be followed by a nonnegative integer that indicates the desired 
        ///   depth buffer size. The smallest available depth buffer of at least the 
        ///   minimum size is preferred. If the desired value is zero, frame buffer 
        ///   configurations with no depth buffer are preferred. 
        /// 
        ///   <para>
        ///     The default value is 0.
        ///   </para>
        /// </summary>
        EGL_DEPTH_SIZE = NativeEgl.EGL_DEPTH_SIZE,

        /// <summary>
        ///   Must be followed by an integer buffer-level specification. 
        ///   This specification is honored exactly. Buffer level 0 corresponds 
        ///   to the default frame buffer of the display. Buffer level 1 is the 
        ///   first overlay frame buffer, level two the second overlay frame buffer, 
        ///   and so on. Negative buffer levels correspond to underlay frame buffers. 
        /// 
        ///   <para>
        ///     The default value is 0.
        ///   </para>
        /// </summary>
        EGL_LEVEL = NativeEgl.EGL_LEVEL,

        /// <summary>
        ///   Must be followed by EGL_DONT_CARE, EGL_TRUE, or EGL_FALSE. 
        ///   If EGL_TRUE is specified, then only frame buffer configurations 
        ///   that allow native rendering into the surface will be considered. 
        /// 
        ///   <para>
        ///     The default value is EGL_DONT_CARE.
        ///   </para>
        /// </summary>
        EGL_NATIVE_RENDERABLE = NativeEgl.EGL_NATIVE_RENDERABLE,

        /// <summary>
        ///   Must be followed by a platform dependent value or EGL_DONT_CARE. 
        ///   The default value is EGL_DONT_CARE.
        /// </summary>
        EGL_NATIVE_VISUAL_TYPE = NativeEgl.EGL_NATIVE_VISUAL_TYPE,

        /// <summary>
        ///   Must be followed by the minimum acceptable number of multisample buffers.
        ///   Configurations with the smallest number of multisample buffers that meet 
        ///   or exceed this minimum number are preferred. Currently operation with more 
        ///   than one multisample buffer is undefined, so only values of zero or one 
        ///   will produce a match. 
        /// 
        ///   <para>
        ///     The default value is 0.
        ///   </para>
        /// </summary>
        EGL_SAMPLE_BUFFERS = NativeEgl.EGL_SAMPLE_BUFFERS,

        /// <summary>
        ///   Must be followed by the minimum number of samples required in multisample 
        ///   buffers. Configurations with the smallest number of samples that meet or 
        ///   exceed the specified minimum number are preferred. Note that it is possible 
        ///   for color samples in the multisample buffer to have fewer bits than colors 
        ///   in the main color buffers. However, multisampled colors maintain at least 
        ///   as much color resolution in aggregate as the main color buffers.
        /// </summary>
        EGL_SAMPLES = NativeEgl.EGL_SAMPLES,

        /// <summary>
        ///   Must be followed by a nonnegative integer that indicates the desired number 
        ///   of stencil bitplanes. The smallest stencil buffer of at least the specified 
        ///   size is preferred. If the desired value is zero, frame buffer configurations 
        ///   with no stencil buffer are preferred. 
        /// 
        ///   <para>
        ///     The default value is 0.
        ///   </para>
        /// </summary>
        EGL_STENCIL_SIZE = NativeEgl.EGL_STENCIL_SIZE,

        /// <summary>
        ///   Must be followed by a mask indicating which EGL surface types the frame 
        ///   buffer configuration must support. Valid bits are EGL_WINDOW_BIT, 
        ///   EGL_PBUFFER_BIT, and EGL_PIXMAP_BIT. For example, if mask is set to 
        ///   EGL_WINDOW_BIT | EGL_PIXMAP_BIT, only frame buffer configurations that 
        ///   support both windows and pixmaps will be considered. 
        /// 
        ///   <para>
        ///     The default value is EGL_WINDOW_BIT.
        ///   </para>
        /// </summary>
        EGL_SURFACE_TYPE = NativeEgl.EGL_SURFACE_TYPE,

        /// <summary>
        ///   Must be followed by one of EGL_NONE or EGL_TRANSPARENT_RGB. If EGL_NONE is 
        ///   specified, then only opaque frame buffer configurations will be considered. 
        ///   If EGL_TRANSPARENT_RGB is specified, then only transparent frame buffer
        ///   configurations will be considered. 
        /// 
        ///   <para>
        ///     The default value is EGL_NONE.
        ///   </para>
        /// </summary>
        EGL_TRANSPARENT_TYPE = NativeEgl.EGL_TRANSPARENT_TYPE,

        /// <summary>
        ///   Must be followed by an integer value indicating the transparent red value. 
        ///   The value must be between 0 and the maximum color buffer value for red. 
        ///   Only frame buffer configurations that use the specified transparent red 
        ///   value will be considered. 
        /// 
        ///   <para>
        ///     The default value is EGL_DONT_CARE.
        ///   </para>
        /// 
        ///   This attribute is ignored unless EGL_TRANSPARENT_TYPE is included in 
        ///   attrib_list and specified as EGL_TRANSPARENT_RGB.
        /// </summary>
        EGL_TRANSPARENT_RED_VALUE = NativeEgl.EGL_TRANSPARENT_RED_VALUE,

        /// <summary>
        ///   Must be followed by an integer value indicating the transparent green value. 
        ///   The value must be between 0 and the maximum color buffer value for red. 
        /// 
        ///   Only frame buffer configurations that use the specified transparent green 
        ///   value will be considered. 
        /// 
        ///   <para>
        ///     The default value is EGL_DONT_CARE.
        ///   </para>
        /// 
        ///   This attribute is ignored unless EGL_TRANSPARENT_TYPE is included in 
        ///   attrib_list and specified as EGL_TRANSPARENT_RGB.
        /// </summary>
        EGL_TRANSPARENT_GREEN_VALUE = NativeEgl.EGL_TRANSPARENT_GREEN_VALUE,

        /// <summary>
        ///   Must be followed by an integer value indicating the transparent blue value. 
        ///   The value must be between 0 and the maximum color buffer value for red.
        /// 
        ///   Only frame buffer configurations that use the specified transparent blue 
        ///   value will be considered. 
        /// 
        ///   <para>
        ///     The default value is EGL_DONT_CARE.
        ///   </para> 
        ///   This attribute is ignored unless EGL_TRANSPARENT_TYPE is included in 
        ///   attrib_list and specified as EGL_TRANSPARENT_RGB.
        /// </summary>
        EGL_TRANSPARENT_BLUE_VALUE = NativeEgl.EGL_TRANSPARENT_BLUE_VALUE
    }
}