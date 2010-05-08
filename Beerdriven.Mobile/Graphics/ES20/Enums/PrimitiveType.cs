namespace Beerdriven.Mobile.Graphics.ES20.Enums
{
    using Interop;

    public enum PrimitiveType : uint
    {
        GL_POINTS = NativeGl.GL_POINTS,
        GL_LINE_STRIP = NativeGl.GL_LINE_STRIP,
        GL_LINE_LOOP = NativeGl.GL_LINE_LOOP,
        GL_LINES = NativeGl.GL_LINES,
        GL_TRIANGLE_STRIP = NativeGl.GL_TRIANGLE_STRIP,
        GL_TRIANGLE_FAN = NativeGl.GL_TRIANGLE_FAN,
        GL_TRIANGLES = NativeGl.GL_TRIANGLES,
    }
}