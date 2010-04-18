namespace SampleApp
{
    using Beerdriven.Mobile.Graphics.ES20;

    public class SampleShader : ShaderProgram
    {
        public uint BaseTexture
        {
            get;
            set;
        }

        public MatrixVariable Projection
        {
            get;
            private set;
        }

        public AttributeVariable TexCoord
        {
            get;
            private set;
        }

        public AttributeVariable Vertex
        {
            get;
            private set;
        }

        public MatrixVariable View
        {
            get;
            private set;
        }

        public MatrixVariable World
        {
            get;
            private set;
        }

        protected override void OnBeforeLinked()
        {
        }

        protected override void OnLinked()
        {
            this.World = new MatrixVariable(this, "u_world");
            this.View = new MatrixVariable(this, "u_view");
            this.Projection = new MatrixVariable(this, "u_projection");

            this.Vertex = new AttributeVariable(this, "a_vertex");
            this.TexCoord = new AttributeVariable(this, "a_texcoord");

            this.BaseTexture = this.GetUniformLocation("t_base");
        }
    }
}