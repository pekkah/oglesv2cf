namespace Beerdriven.Mobile.Graphics.ES20.Enums
{
    using Interop;

    public enum BufferUsage
    {
        /// <summary>
        /// The data store contents will be modified once
        /// and used many times as the source for GL drawing commands.
        /// </summary>
        GL_STATIC_DRAW = NativeGl.GL_STATIC_DRAW,

        /// <summary>
        /// The data store contents will be modified repeatedly
        /// and used many times as the source for GL drawing commands.
        /// </summary>
        GL_DYNAMIC_DRAW = NativeGl.GL_DYNAMIC_DRAW
    }
}