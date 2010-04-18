namespace SampleApp
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public float X, Y, Z; // position

        public float S, T;

        public Vertex(float x, float y, float z, float s, float t)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.S = s;
            this.T = t;
        }
    }
}