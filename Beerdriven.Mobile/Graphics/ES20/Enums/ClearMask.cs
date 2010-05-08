namespace Beerdriven.Mobile.Graphics.ES20.Enums
{
    using System;
    using Interop;

    /// <summary>
    /// Bitwise OR of masks that indicate the buffers to be cleared.
    /// </summary>
    [Flags]
    public enum ClearMask : uint
    {
        GL_COLOR_BUFFER_BIT = NativeGl.GL_COLOR_BUFFER_BIT,

        GL_DEPTH_BUFFER_BIT = NativeGl.GL_DEPTH_BUFFER_BIT,

        GL_STENCIL_BUFFER_BIT = NativeGl.GL_STENCIL_BUFFER_BIT
    }
}
