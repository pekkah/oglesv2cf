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

namespace Beerdriven.Mobile.Graphics.ES20.Interop
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class NativeGl
    {
        public const int GL_ACTIVE_ATTRIBUTES = 0x8B89;

        public const int GL_ACTIVE_ATTRIBUTE_MAX_LENGTH = 0x8B8A;

        public const int GL_ACTIVE_TEXTURE = 0x84E0;

        public const int GL_ACTIVE_UNIFORMS = 0x8B86;

        public const int GL_ACTIVE_UNIFORM_MAX_LENGTH = 0x8B87;

        public const int GL_ALIASED_LINE_WIDTH_RANGE = 0x846E;

        public const int GL_ALIASED_POINT_SIZE_RANGE = 0x846D;

        public const int GL_ALPHA = 0x1906;

        public const int GL_ALPHA_BITS = 0x0D55;

        public const int GL_ALWAYS = 0x0207;

        public const int GL_ARRAY_BUFFER = 0x8892;

        public const int GL_ARRAY_BUFFER_BINDING = 0x8894;

        public const int GL_ATTACHED_SHADERS = 0x8B85;

        public const int GL_BACK = 0x0405;

        public const int GL_BLEND = 0x0BE2;

        public const int GL_BLEND_COLOR = 0x8005;

        public const int GL_BLEND_DST_ALPHA = 0x80CA;

        public const int GL_BLEND_DST_RGB = 0x80C8;

        public const int GL_BLEND_EQUATION = 0x8009;

        public const int GL_BLEND_EQUATION_ALPHA = 0x883D;

        public const int GL_BLEND_EQUATION_RGB = 0x8009; /* same as BLEND_EQUATION */

        /* BlendSubtract */

        public const int GL_BLEND_SRC_ALPHA = 0x80CB;

        public const int GL_BLEND_SRC_RGB = 0x80C9;

        public const int GL_BLUE_BITS = 0x0D54;

        public const int GL_BOOL = 0x8B56;

        public const int GL_BOOL_VEC2 = 0x8B57;

        public const int GL_BOOL_VEC3 = 0x8B58;

        public const int GL_BOOL_VEC4 = 0x8B59;

        public const int GL_BUFFER_SIZE = 0x8764;

        public const int GL_BUFFER_USAGE = 0x8765;

        public const int GL_BYTE = 0x1400;

        public const int GL_CCW = 0x0901;

        public const int GL_CLAMP_TO_EDGE = 0x812F;

        public const int GL_COLOR_ATTACHMENT0 = 0x8CE0;

        public const int GL_COLOR_BUFFER_BIT = 0x00004000;

        /* GetPName */

        /*      GL_SCISSOR_TEST */

        public const int GL_COLOR_CLEAR_VALUE = 0x0C22;

        public const int GL_COLOR_WRITEMASK = 0x0C23;

        public const int GL_COMPILE_STATUS = 0x8B81;

        public const int GL_COMPRESSED_TEXTURE_FORMATS = 0x86A3;

        public const int GL_CONSTANT_ALPHA = 0x8003;

        public const int GL_CONSTANT_COLOR = 0x8001;

        public const int GL_CULL_FACE = 0x0B44;

        public const int GL_CULL_FACE_MODE = 0x0B45;

        public const int GL_CURRENT_PROGRAM = 0x8B8D;

        public const int GL_CURRENT_VERTEX_ATTRIB = 0x8626;

        public const int GL_CW = 0x0900;

        public const int GL_DECR = 0x1E03;

        public const int GL_DECR_WRAP = 0x8508;

        public const int GL_DELETE_STATUS = 0x8B80;

        public const int GL_DEPTH_ATTACHMENT = 0x8D00;

        public const int GL_DEPTH_BITS = 0x0D56;

        public const int GL_DEPTH_BUFFER_BIT = 0x00000100;

        public const int GL_DEPTH_CLEAR_VALUE = 0x0B73;

        public const int GL_DEPTH_COMPONENT = 0x1902;

        public const int GL_DEPTH_COMPONENT16 = 0x81A5;

        public const int GL_DEPTH_FUNC = 0x0B74;

        public const int GL_DEPTH_RANGE = 0x0B70;

        public const int GL_DEPTH_TEST = 0x0B71;

        public const int GL_DEPTH_WRITEMASK = 0x0B72;

        public const int GL_DITHER = 0x0BD0;

        /* HintMode */

        public const int GL_DONT_CARE = 0x1100;

        public const int GL_DST_ALPHA = 0x0304;

        public const int GL_DST_COLOR = 0x0306;

        public const int GL_DYNAMIC_DRAW = 0x88E8;

        public const int GL_ELEMENT_ARRAY_BUFFER = 0x8893;

        public const int GL_ELEMENT_ARRAY_BUFFER_BINDING = 0x8895;

        public const int GL_EQUAL = 0x0202;

        public const int GL_ES_VERSION_2_0 = 1;

        public const int GL_EXTENSIONS = 0x1F03;

        public const int GL_FALSE = 0;

        public const int GL_FASTEST = 0x1101;

        public const int GL_FIXED = 0x140C;

        public const int GL_FLOAT = 0x1406;

        public const int GL_FLOAT_MAT2 = 0x8B5A;

        public const int GL_FLOAT_MAT3 = 0x8B5B;

        public const int GL_FLOAT_MAT4 = 0x8B5C;

        public const int GL_FLOAT_VEC2 = 0x8B50;

        public const int GL_FLOAT_VEC3 = 0x8B51;

        public const int GL_FLOAT_VEC4 = 0x8B52;

        public const int GL_FRAGMENT_SHADER = 0x8B30;

        public const int GL_FRAMEBUFFER = 0x8D40;

        public const int GL_FRAMEBUFFER_ATTACHMENT_OBJECT_NAME = 0x8CD1;

        public const int GL_FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE = 0x8CD0;

        public const int GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE = 0x8CD3;

        public const int GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL = 0x8CD2;

        public const int GL_FRAMEBUFFER_BINDING = 0x8CA6;

        public const int GL_FRAMEBUFFER_COMPLETE = 0x8CD5;

        public const int GL_FRAMEBUFFER_INCOMPLETE_ATTACHMENT = 0x8CD6;

        public const int GL_FRAMEBUFFER_INCOMPLETE_DIMENSIONS = 0x8CD9;

        public const int GL_FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT = 0x8CD7;

        public const int GL_FRAMEBUFFER_UNSUPPORTED = 0x8CDD;

        public const int GL_FRONT = 0x0404;

        public const int GL_FRONT_AND_BACK = 0x0408;

        public const int GL_FRONT_FACE = 0x0B46;

        public const int GL_FUNC_ADD = 0x8006;

        public const int GL_FUNC_REVERSE_SUBTRACT = 0x800B;

        public const int GL_FUNC_SUBTRACT = 0x800A;

        /* HintTarget */

        public const int GL_GENERATE_MIPMAP_HINT = 0x8192;

        public const int GL_GEQUAL = 0x0206;

        public const int GL_GREATER = 0x0204;

        public const int GL_GREEN_BITS = 0x0D53;

        public const int GL_HIGH_FLOAT = 0x8DF2;

        public const int GL_HIGH_INT = 0x8DF5;

        public const int GL_IMPLEMENTATION_COLOR_READ_FORMAT = 0x8B9B;

        public const int GL_IMPLEMENTATION_COLOR_READ_TYPE = 0x8B9A;

        public const int GL_INCR = 0x1E02;

        public const int GL_INCR_WRAP = 0x8507;

        public const int GL_INFO_LOG_LENGTH = 0x8B84;

        /* DataType */

        public const int GL_INT = 0x1404;

        public const int GL_INT_VEC2 = 0x8B53;

        public const int GL_INT_VEC3 = 0x8B54;

        public const int GL_INT_VEC4 = 0x8B55;

        public const int GL_INVALID_ENUM = 0x0500;

        public const int GL_INVALID_FRAMEBUFFER_OPERATION = 0x0506;

        public const int GL_INVALID_OPERATION = 0x0502;

        public const int GL_INVALID_VALUE = 0x0501;

        public const int GL_INVERT = 0x150A;

        public const int GL_KEEP = 0x1E00;

        public const int GL_LEQUAL = 0x0203;

        public const int GL_LESS = 0x0201;

        public const int GL_LINEAR = 0x2601;

        public const int GL_LINEAR_MIPMAP_LINEAR = 0x2703;

        public const int GL_LINEAR_MIPMAP_NEAREST = 0x2701;

        public const int GL_LINES = 0x0001;

        public const int GL_LINE_LOOP = 0x0002;

        public const int GL_LINE_STRIP = 0x0003;

        public const int GL_LINE_WIDTH = 0x0B21;

        public const int GL_LINK_STATUS = 0x8B82;

        public const int GL_LOW_FLOAT = 0x8DF0;

        public const int GL_LOW_INT = 0x8DF3;

        public const int GL_LUMINANCE = 0x1909;

        public const int GL_LUMINANCE_ALPHA = 0x190A;

        /* PixelType */
        /*      GL_UNSIGNED_BYTE */

        public const int GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS = 0x8B4D;

        public const int GL_MAX_CUBE_MAP_TEXTURE_SIZE = 0x851C;

        public const int GL_MAX_FRAGMENT_UNIFORM_VECTORS = 0x8DFD;

        public const int GL_MAX_RENDERBUFFER_SIZE = 0x84E8;

        public const int GL_MAX_TEXTURE_IMAGE_UNITS = 0x8872;

        public const int GL_MAX_TEXTURE_SIZE = 0x0D33;

        public const int GL_MAX_VARYING_VECTORS = 0x8DFC;

        public const int GL_MAX_VERTEX_ATTRIBS = 0x8869;

        public const int GL_MAX_VERTEX_TEXTURE_IMAGE_UNITS = 0x8B4C;

        public const int GL_MAX_VERTEX_UNIFORM_VECTORS = 0x8DFB;

        public const int GL_MAX_VIEWPORT_DIMS = 0x0D3A;

        public const int GL_MEDIUM_FLOAT = 0x8DF1;

        public const int GL_MEDIUM_INT = 0x8DF4;

        public const int GL_MIRRORED_REPEAT = 0x8370;

        public const int GL_NEAREST = 0x2600;

        public const int GL_NEAREST_MIPMAP_LINEAR = 0x2702;

        public const int GL_NEAREST_MIPMAP_NEAREST = 0x2700;

        /* StencilFunction */

        public const int GL_NEVER = 0x0200;

        public const int GL_NICEST = 0x1102;

        public const int GL_NONE = 0;

        public const int GL_NOTEQUAL = 0x0205;

        public const int GL_NO_ERROR = 0;

        public const int GL_NUM_COMPRESSED_TEXTURE_FORMATS = 0x86A2;

        public const int GL_NUM_SHADER_BINARY_FORMATS = 0x8DF9;

        public const int GL_ONE = 1;

        public const int GL_ONE_MINUS_CONSTANT_ALPHA = 0x8004;

        public const int GL_ONE_MINUS_CONSTANT_COLOR = 0x8002;

        public const int GL_ONE_MINUS_DST_ALPHA = 0x0305;

        public const int GL_ONE_MINUS_DST_COLOR = 0x0307;

        public const int GL_ONE_MINUS_SRC_ALPHA = 0x0303;

        public const int GL_ONE_MINUS_SRC_COLOR = 0x0301;

        public const int GL_OUT_OF_MEMORY = 0x0505;

        public const int GL_PACK_ALIGNMENT = 0x0D05;

        public const int GL_POINTS = 0x0000;

        public const int GL_POLYGON_OFFSET_FACTOR = 0x8038;

        public const int GL_POLYGON_OFFSET_FILL = 0x8037;

        public const int GL_POLYGON_OFFSET_UNITS = 0x2A00;

        public const int GL_RED_BITS = 0x0D52;

        public const int GL_RENDERBUFFER = 0x8D41;

        public const int GL_RENDERBUFFER_ALPHA_SIZE = 0x8D53;

        public const int GL_RENDERBUFFER_BINDING = 0x8CA7;

        public const int GL_RENDERBUFFER_BLUE_SIZE = 0x8D52;

        public const int GL_RENDERBUFFER_DEPTH_SIZE = 0x8D54;

        public const int GL_RENDERBUFFER_GREEN_SIZE = 0x8D51;

        public const int GL_RENDERBUFFER_HEIGHT = 0x8D43;

        public const int GL_RENDERBUFFER_INTERNAL_FORMAT = 0x8D44;

        public const int GL_RENDERBUFFER_RED_SIZE = 0x8D50;

        public const int GL_RENDERBUFFER_STENCIL_SIZE = 0x8D55;

        public const int GL_RENDERBUFFER_WIDTH = 0x8D42;

        public const int GL_RENDERER = 0x1F01;

        public const int GL_REPEAT = 0x2901;

        public const int GL_REPLACE = 0x1E01;

        public const int GL_RGB = 0x1907;

        public const int GL_RGB565 = 0x8D62;

        public const int GL_RGB5_A1 = 0x8057;

        public const int GL_RGBA = 0x1908;

        public const int GL_RGBA4 = 0x8056;

        public const int GL_SAMPLER_2D = 0x8B5E;

        public const int GL_SAMPLER_CUBE = 0x8B60;

        public const int GL_SAMPLES = 0x80A9;

        public const int GL_SAMPLE_ALPHA_TO_COVERAGE = 0x809E;

        public const int GL_SAMPLE_BUFFERS = 0x80A8;

        public const int GL_SAMPLE_COVERAGE = 0x80A0;

        public const int GL_SAMPLE_COVERAGE_INVERT = 0x80AB;

        public const int GL_SAMPLE_COVERAGE_VALUE = 0x80AA;

        public const int GL_SCISSOR_BOX = 0x0C10;

        public const int GL_SCISSOR_TEST = 0x0C11;

        public const int GL_SHADER_BINARY_FORMATS = 0x8DF8;

        public const int GL_SHADER_COMPILER = 0x8DFA;

        public const int GL_SHADER_SOURCE_LENGTH = 0x8B88;

        public const int GL_SHADER_TYPE = 0x8B4F;

        public const int GL_SHADING_LANGUAGE_VERSION = 0x8B8C;

        public const int GL_SHORT = 0x1402;

        public const int GL_SRC_ALPHA = 0x0302;

        public const int GL_SRC_ALPHA_SATURATE = 0x0308;

        public const int GL_SRC_COLOR = 0x0300;

        public const int GL_STATIC_DRAW = 0x88E4;

        public const int GL_STENCIL_ATTACHMENT = 0x8D20;

        public const int GL_STENCIL_BACK_FAIL = 0x8801;

        public const int GL_STENCIL_BACK_FUNC = 0x8800;

        public const int GL_STENCIL_BACK_PASS_DEPTH_FAIL = 0x8802;

        public const int GL_STENCIL_BACK_PASS_DEPTH_PASS = 0x8803;

        public const int GL_STENCIL_BACK_REF = 0x8CA3;

        public const int GL_STENCIL_BACK_VALUE_MASK = 0x8CA4;

        public const int GL_STENCIL_BACK_WRITEMASK = 0x8CA5;

        public const int GL_STENCIL_BITS = 0x0D57;

        public const int GL_STENCIL_BUFFER_BIT = 0x00000400;

        public const int GL_STENCIL_CLEAR_VALUE = 0x0B91;

        public const int GL_STENCIL_FAIL = 0x0B94;

        public const int GL_STENCIL_FUNC = 0x0B92;

        public const int GL_STENCIL_INDEX = 0x1901;

        public const int GL_STENCIL_INDEX8 = 0x8D48;

        public const int GL_STENCIL_PASS_DEPTH_FAIL = 0x0B95;

        public const int GL_STENCIL_PASS_DEPTH_PASS = 0x0B96;

        public const int GL_STENCIL_REF = 0x0B97;

        public const int GL_STENCIL_TEST = 0x0B90;

        public const int GL_STENCIL_VALUE_MASK = 0x0B93;

        public const int GL_STENCIL_WRITEMASK = 0x0B98;

        public const int GL_STREAM_DRAW = 0x88E0;

        public const int GL_SUBPIXEL_BITS = 0x0D50;

        /* TextureTarget */
        /*      GL_TEXTURE_2D */

        public const int GL_TEXTURE = 0x1702;

        /* TextureUnit */

        public const int GL_TEXTURE0 = 0x84C0;

        public const int GL_TEXTURE1 = 0x84C1;

        public const int GL_TEXTURE10 = 0x84CA;

        public const int GL_TEXTURE11 = 0x84CB;

        public const int GL_TEXTURE12 = 0x84CC;

        public const int GL_TEXTURE13 = 0x84CD;

        public const int GL_TEXTURE14 = 0x84CE;

        public const int GL_TEXTURE15 = 0x84CF;

        public const int GL_TEXTURE16 = 0x84D0;

        public const int GL_TEXTURE17 = 0x84D1;

        public const int GL_TEXTURE18 = 0x84D2;

        public const int GL_TEXTURE19 = 0x84D3;

        public const int GL_TEXTURE2 = 0x84C2;

        public const int GL_TEXTURE20 = 0x84D4;

        public const int GL_TEXTURE21 = 0x84D5;

        public const int GL_TEXTURE22 = 0x84D6;

        public const int GL_TEXTURE23 = 0x84D7;

        public const int GL_TEXTURE24 = 0x84D8;

        public const int GL_TEXTURE25 = 0x84D9;

        public const int GL_TEXTURE26 = 0x84DA;

        public const int GL_TEXTURE27 = 0x84DB;

        public const int GL_TEXTURE28 = 0x84DC;

        public const int GL_TEXTURE29 = 0x84DD;

        public const int GL_TEXTURE3 = 0x84C3;

        public const int GL_TEXTURE30 = 0x84DE;

        public const int GL_TEXTURE31 = 0x84DF;

        public const int GL_TEXTURE4 = 0x84C4;

        public const int GL_TEXTURE5 = 0x84C5;

        public const int GL_TEXTURE6 = 0x84C6;

        public const int GL_TEXTURE7 = 0x84C7;

        public const int GL_TEXTURE8 = 0x84C8;

        public const int GL_TEXTURE9 = 0x84C9;

        public const int GL_TEXTURE_2D = 0x0DE1;

        public const int GL_TEXTURE_BINDING_2D = 0x8069;

        public const int GL_TEXTURE_BINDING_CUBE_MAP = 0x8514;

        public const int GL_TEXTURE_CUBE_MAP = 0x8513;

        public const int GL_TEXTURE_CUBE_MAP_NEGATIVE_X = 0x8516;

        public const int GL_TEXTURE_CUBE_MAP_NEGATIVE_Y = 0x8518;

        public const int GL_TEXTURE_CUBE_MAP_NEGATIVE_Z = 0x851A;

        public const int GL_TEXTURE_CUBE_MAP_POSITIVE_X = 0x8515;

        public const int GL_TEXTURE_CUBE_MAP_POSITIVE_Y = 0x8517;

        public const int GL_TEXTURE_CUBE_MAP_POSITIVE_Z = 0x8519;

        public const int GL_TEXTURE_MAG_FILTER = 0x2800;

        public const int GL_TEXTURE_MIN_FILTER = 0x2801;

        public const int GL_TEXTURE_WRAP_S = 0x2802;

        public const int GL_TEXTURE_WRAP_T = 0x2803;

        public const int GL_TRIANGLES = 0x0004;

        public const int GL_TRIANGLE_FAN = 0x0006;

        public const int GL_TRIANGLE_STRIP = 0x0005;

        public const int GL_TRUE = 1;

        public const int GL_UNPACK_ALIGNMENT = 0x0CF5;

        public const int GL_UNSIGNED_BYTE = 0x1401;

        public const int GL_UNSIGNED_INT = 0x1405;

        public const int GL_UNSIGNED_SHORT = 0x1403;

        public const int GL_UNSIGNED_SHORT_4_4_4_4 = 0x8033;

        public const int GL_UNSIGNED_SHORT_5_5_5_1 = 0x8034;

        public const int GL_UNSIGNED_SHORT_5_6_5 = 0x8363;

        public const int GL_VALIDATE_STATUS = 0x8B83;

        public const int GL_VENDOR = 0x1F00;

        public const int GL_VERSION = 0x1F02;

        public const int GL_VERTEX_ATTRIB_ARRAY_BUFFER_BINDING = 0x889F;

        /* Vertex Arrays */

        public const int GL_VERTEX_ATTRIB_ARRAY_ENABLED = 0x8622;

        public const int GL_VERTEX_ATTRIB_ARRAY_NORMALIZED = 0x886A;

        public const int GL_VERTEX_ATTRIB_ARRAY_POINTER = 0x8645;

        public const int GL_VERTEX_ATTRIB_ARRAY_SIZE = 0x8623;

        public const int GL_VERTEX_ATTRIB_ARRAY_STRIDE = 0x8624;

        public const int GL_VERTEX_ATTRIB_ARRAY_TYPE = 0x8625;

        public const int GL_VERTEX_SHADER = 0x8B31;

        public const int GL_VIEWPORT = 0x0BA2;

        public const int GL_ZERO = 0;

        public const string gl2LibraryName = "libGLESv2.dll";

        /*-------------------------------------------------------------------------
         * GL core functions.
         *-----------------------------------------------------------------------*/

        [DllImport(gl2LibraryName)]
        public static extern void glActiveTexture(uint texture);

        [DllImport(gl2LibraryName)]
        public static extern void glAttachShader(uint program, uint shader);

        [DllImport(gl2LibraryName)]
        public static extern void glBindAttribLocation(uint program, uint index, IntPtr name);

        // const char* name);

        [DllImport(gl2LibraryName)]
        public static extern void glBindBuffer(uint target, uint buffer);

        [DllImport(gl2LibraryName)]
        public static extern void glBindFramebuffer(uint target, uint framebuffer);

        [DllImport(gl2LibraryName)]
        public static extern void glBindRenderbuffer(uint target, uint renderbuffer);

        [DllImport(gl2LibraryName)]
        public static extern void glBindTexture(uint target, uint texture);

        [DllImport(gl2LibraryName)]
        public static extern void glBlendColor(float red, float green, float blue, float alpha);

        [DllImport(gl2LibraryName)]
        public static extern void glBlendEquation(uint mode);

        [DllImport(gl2LibraryName)]
        public static extern void glBlendEquationSeparate(uint modeRGB, uint modeAlpha);

        [DllImport(gl2LibraryName)]
        public static extern void glBlendFunc(uint sfactor, uint dfactor);

        [DllImport(gl2LibraryName)]
        public static extern void glBlendFuncSeparate(uint srcRGB, uint dstRGB, uint srcAlpha, uint dstAlpha);

        [DllImport(gl2LibraryName)]
        public static extern void glBufferData(uint target, IntPtr size, IntPtr data, uint usage);

        [DllImport(gl2LibraryName)]
        public static extern void glBufferSubData(uint target, long offset, long size, IntPtr data);

        [DllImport(gl2LibraryName)]
        public static extern uint glCheckFramebufferStatus(uint target);

        [DllImport(gl2LibraryName)]
        public static extern void glClear(uint mask);

        [DllImport(gl2LibraryName)]
        public static extern void glClearColor(float red, float green, float blue, float alpha);

        [DllImport(gl2LibraryName)]
        public static extern void glClearDepthf(float depth);

        [DllImport(gl2LibraryName)]
        public static extern void glClearStencil(int s);

        [DllImport(gl2LibraryName)]
        public static extern void glColorMask(byte red, byte green, byte blue, byte alpha);

        [DllImport(gl2LibraryName)]
        public static extern void glCompileShader(uint shader);

        [DllImport(gl2LibraryName)]
        public static extern void glCompressedTexImage2D(
                uint target,
                int level,
                uint internalformat,
                int width,
                int height,
                int border,
                int imageSize,
                IntPtr data);

        [DllImport(gl2LibraryName)]
        public static extern void glCompressedTexSubImage2D(
                uint target,
                int level,
                int xoffset,
                int yoffset,
                int width,
                int height,
                uint format,
                int imageSize,
                IntPtr data);

        [DllImport(gl2LibraryName)]
        public static extern void glCopyTexImage2D(
                uint target, int level, uint internalformat, int x, int y, int width, int height, int border);

        [DllImport(gl2LibraryName)]
        public static extern void glCopyTexSubImage2D(
                uint target, int level, int xoffset, int yoffset, int x, int y, int width, int height);

        [DllImport(gl2LibraryName)]
        public static extern uint glCreateProgram();

        [DllImport(gl2LibraryName)]
        public static extern uint glCreateShader(uint type);

        [DllImport(gl2LibraryName)]
        public static extern void glCullFace(uint mode);

        [DllImport(gl2LibraryName)]
        public static extern void glDeleteBuffers(int n, uint[] buffers);

        //const uint* buffers);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glDeleteFramebuffers(int n, uint* buffers);

        //const uint* framebuffers);

        [DllImport(gl2LibraryName)]
        public static extern void glDeleteProgram(uint program);

        [DllImport(gl2LibraryName)]
        public static extern void glDeleteRenderbuffers(int n, ref uint[] renderbuffers);

        [DllImport(gl2LibraryName)]
        public static extern void glDeleteShader(uint shader);

        [DllImport(gl2LibraryName)]
        public static extern void glDeleteTextures(int n, ref uint[] textures);

        [DllImport(gl2LibraryName)]
        public static extern void glDepthFunc(uint func);

        [DllImport(gl2LibraryName)]
        public static extern void glDepthMask(byte flag);

        [DllImport(gl2LibraryName)]
        public static extern void glDepthRangef(float zNear, float zFar);

        [DllImport(gl2LibraryName)]
        public static extern void glDetachShader(uint program, uint shader);

        [DllImport(gl2LibraryName)]
        public static extern void glDisable(uint cap);

        [DllImport(gl2LibraryName)]
        public static extern void glDisableVertexAttribArray(uint index);

        [DllImport(gl2LibraryName)]
        public static extern void glDrawArrays(uint mode, int first, int count);

        [DllImport(gl2LibraryName)]
        public static extern void glDrawElements(uint mode, int count, uint type, IntPtr indices);

        [DllImport(gl2LibraryName)]
        public static extern void glEnable(uint cap);

        [DllImport(gl2LibraryName)]
        public static extern void glEnableVertexAttribArray(uint index);

        [DllImport(gl2LibraryName)]
        public static extern void glFinish();

        [DllImport(gl2LibraryName)]
        public static extern void glFlush();

        [DllImport(gl2LibraryName)]
        public static extern void glFramebufferRenderbuffer(
                uint target, uint attachment, uint renderbuffertarget, uint renderbuffer);

        [DllImport(gl2LibraryName)]
        public static extern void glFramebufferTexture2D(
                uint target, uint attachment, uint textarget, uint texture, int level);

        [DllImport(gl2LibraryName)]
        public static extern void glFrontFace(uint mode);

        [DllImport(gl2LibraryName)]
        public static extern void glGenBuffers(int n, uint[] buffers);

        // uint*

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGenFramebuffers(int n, uint* framebuffers);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGenRenderbuffers(int n, uint* renderbuffers);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGenTextures(int n, uint* textures);

        [DllImport(gl2LibraryName)]
        public static extern void glGenerateMipmap(uint target);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGetActiveAttrib(
                uint program, uint index, int bufsize, int* length, int* size, uint* type, char* name);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGetActiveUniform(
                uint program, uint index, int bufsize, int* length, int* size, uint* type, char* name);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGetAttachedShaders(uint program, int maxcount, int* count, uint* shaders);

        [DllImport(gl2LibraryName)]
        public static extern uint glGetAttribLocation(uint program, IntPtr name);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGetBooleanv(uint pname, byte* @params);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGetBufferParameteriv(uint target, uint pname, int* @params);

        [DllImport(gl2LibraryName)]
        public static extern uint glGetError();

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGetFloatv(uint pname, float* @params);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGetFramebufferAttachmentParameteriv(
                uint target, uint attachment, uint pname, int* @params);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGetIntegerv(uint pname, int* @params);

        [DllImport(gl2LibraryName)]
        public static extern void glGetProgramInfoLog(uint program, int bufsize, out int length, char[] infolog);

        [DllImport(gl2LibraryName)]
        public static extern void glGetProgramiv(uint program, uint pname, int[] @params);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGetRenderbufferParameteriv(uint target, uint pname, int* @params);

        // int* @params);

        [DllImport(gl2LibraryName)]
        public static extern void glGetShaderInfoLog(uint shader, int bufsize, out int length, char[] infoLog);

        // int* length, char* infolog);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGetShaderPrecisionFormat(
                uint shadertype, uint precisiontype, int* range, int* precision);

        [DllImport(gl2LibraryName)]
        public static extern void glGetShaderSource(uint shader, int bufsize, out int length, StringBuilder source);

        [DllImport(gl2LibraryName)]
        public static extern void glGetShaderiv(uint shader, uint pname, int[] @params);

        [DllImport(gl2LibraryName)]
        public static extern IntPtr glGetString(uint name);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGetTexParameterfv(uint target, uint pname, float* @params);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGetTexParameteriv(uint target, uint pname, int* @params);

        [DllImport(gl2LibraryName)]
        public static extern uint glGetUniformLocation(uint program, IntPtr name);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGetUniformfv(uint program, int location, float* @params);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGetUniformiv(uint program, int location, int* @params);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGetVertexAttribPointerv(uint index, uint pname, void** pointer);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGetVertexAttribfv(uint index, uint pname, float* @params);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glGetVertexAttribiv(uint index, uint pname, int* @params);

        [DllImport(gl2LibraryName)]
        public static extern void glHint(uint target, uint mode);

        [DllImport(gl2LibraryName)]
        public static extern byte glIsBuffer(uint buffer);

        [DllImport(gl2LibraryName)]
        public static extern byte glIsEnabled(uint cap);

        [DllImport(gl2LibraryName)]
        public static extern byte glIsFramebuffer(uint framebuffer);

        [DllImport(gl2LibraryName)]
        public static extern byte glIsProgram(uint program);

        [DllImport(gl2LibraryName)]
        public static extern byte glIsRenderbuffer(uint renderbuffer);

        [DllImport(gl2LibraryName)]
        public static extern byte glIsShader(uint shader);

        [DllImport(gl2LibraryName)]
        public static extern byte glIsTexture(uint texture);

        [DllImport(gl2LibraryName)]
        public static extern void glLineWidth(float width);

        [DllImport(gl2LibraryName)]
        public static extern void glLinkProgram(uint program);

        [DllImport(gl2LibraryName)]
        public static extern void glPixelStorei(uint pname, int param);

        [DllImport(gl2LibraryName)]
        public static extern void glPolygonOffset(float factor, float units);

        [DllImport(gl2LibraryName)]
        public static extern void glReadPixels(
                int x, int y, int width, int height, uint format, uint type, IntPtr pixels);

        [DllImport(gl2LibraryName)]
        public static extern void glReleaseShaderCompiler();

        [DllImport(gl2LibraryName)]
        public static extern void glRenderbufferStorage(uint target, uint internalformat, int width, int height);

        [DllImport(gl2LibraryName)]
        public static extern void glSampleCoverage(float value, byte invert);

        [DllImport(gl2LibraryName)]
        public static extern void glScissor(int x, int y, int width, int height);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glShaderBinary(
                int n, uint* shaders, uint binaryformat, IntPtr binary, int length);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glShaderSource(
                UInt32 shader, Int32 count, IntPtr[] shaderString, Int32* length);

        // char** @string, int* length

        [DllImport(gl2LibraryName)]
        public static extern void glStencilFunc(uint func, int @ref, uint mask);

        [DllImport(gl2LibraryName)]
        public static extern void glStencilFuncSeparate(uint face, uint func, int @ref, uint mask);

        [DllImport(gl2LibraryName)]
        public static extern void glStencilMask(uint mask);

        [DllImport(gl2LibraryName)]
        public static extern void glStencilMaskSeparate(uint face, uint mask);

        [DllImport(gl2LibraryName)]
        public static extern void glStencilOp(uint fail, uint zfail, uint zpass);

        [DllImport(gl2LibraryName)]
        public static extern void glStencilOpSeparate(uint face, uint fail, uint zfail, uint zpass);

        [DllImport(gl2LibraryName)]
        public static extern void glTexImage2D(
                uint target,
                int level,
                int internalformat,
                int width,
                int height,
                int border,
                uint format,
                uint type,
                IntPtr pixels);

        [DllImport(gl2LibraryName)]
        public static extern void glTexParameterf(uint target, uint pname, float param);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glTexParameterfv(uint target, uint pname, float* @params);

        [DllImport(gl2LibraryName)]
        public static extern void glTexParameteri(uint target, uint pname, int param);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glTexParameteriv(uint target, uint pname, int* @params);

        [DllImport(gl2LibraryName)]
        public static extern void glTexSubImage2D(
                uint target,
                int level,
                int xoffset,
                int yoffset,
                int width,
                int height,
                uint format,
                uint type,
                IntPtr pixels);

        [DllImport(gl2LibraryName)]
        public static extern void glUniform1f(int location, float x);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glUniform1fv(int location, int count, float* v);

        [DllImport(gl2LibraryName)]
        public static extern void glUniform1i(int location, int x);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glUniform1iv(int location, int count, int* v);

        [DllImport(gl2LibraryName)]
        public static extern void glUniform2f(int location, float x, float y);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glUniform2fv(int location, int count, float* v);

        [DllImport(gl2LibraryName)]
        public static extern void glUniform2i(int location, int x, int y);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glUniform2iv(int location, int count, int* v);

        [DllImport(gl2LibraryName)]
        public static extern void glUniform3f(int location, float x, float y, float z);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glUniform3fv(int location, int count, float* v);

        [DllImport(gl2LibraryName)]
        public static extern void glUniform3i(int location, int x, int y, int z);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glUniform3iv(int location, int count, int* v);

        [DllImport(gl2LibraryName)]
        public static extern void glUniform4f(int location, float x, float y, float z, float w);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glUniform4fv(int location, int count, float* v);

        [DllImport(gl2LibraryName)]
        public static extern void glUniform4i(int location, int x, int y, int z, int w);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glUniform4iv(int location, int count, int* v);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glUniformMatrix2fv(uint location, int count, byte transpose, float* value);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glUniformMatrix3fv(uint location, int count, byte transpose, float* value);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glUniformMatrix4fv(uint location, int count, byte transpose, float* value);

        // float* value

        [DllImport(gl2LibraryName)]
        public static extern void glUseProgram(uint program);

        [DllImport(gl2LibraryName)]
        public static extern void glValidateProgram(uint program);

        [DllImport(gl2LibraryName)]
        public static extern void glVertexAttrib1f(uint indx, float x);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glVertexAttrib1fv(uint indx, float* values);

        [DllImport(gl2LibraryName)]
        public static extern void glVertexAttrib2f(uint indx, float x, float y);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glVertexAttrib2fv(uint indx, float* values);

        [DllImport(gl2LibraryName)]
        public static extern void glVertexAttrib3f(uint indx, float x, float y, float z);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glVertexAttrib3fv(uint indx, float* values);

        [DllImport(gl2LibraryName)]
        public static extern void glVertexAttrib4f(uint indx, float x, float y, float z, float w);

        [DllImport(gl2LibraryName)]
        public static extern unsafe void glVertexAttrib4fv(uint indx, float* values);

        [DllImport(gl2LibraryName)]
        public static extern void glVertexAttribPointer(
                uint indx, int size, uint type, byte normalized, int stride, IntPtr ptr);

        [DllImport(gl2LibraryName)]
        public static extern void glViewport(int x, int y, int width, int height);
    }
}