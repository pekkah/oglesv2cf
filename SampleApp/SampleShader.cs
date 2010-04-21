namespace SampleApp
{
    using Beerdriven.Mobile.Graphics.ES20;

    public class SampleShader : ShaderProgram
    {
        public TextureUniform BaseTexture
        {
            get;
            set;
        }

        public MatrixUniform Projection
        {
            get;
            private set;
        }

        public Attribute TexCoord
        {
            get;
            private set;
        }

        public Attribute Vertex
        {
            get;
            private set;
        }

        public MatrixUniform View
        {
            get;
            private set;
        }

        public MatrixUniform World
        {
            get;
            private set;
        }

        protected override void OnBeforeLinked()
        {
        }

        protected override void OnLinked()
        {
            this.World = new MatrixUniform(this, "u_world");
            this.View = new MatrixUniform(this, "u_view");
            this.Projection = new MatrixUniform(this, "u_projection");

            this.Vertex = new Attribute(this, "a_vertex");
            this.TexCoord = new Attribute(this, "a_texcoord");

            this.BaseTexture = new TextureUniform(this, "t_base");
        }
    }
}