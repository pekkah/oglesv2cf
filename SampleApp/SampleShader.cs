namespace SampleApp
{
    using Beerdriven.Mobile.Graphics.ES20;

    public class SampleShader : glShaderProgram
    {
        public uint PositionHandle;

        public uint ViewHandle;

        public uint ProjectionHandle;

        public uint WorldHandle;

        protected override void OnBeforeLinked()
        {
        }

        protected override void OnLinked()
        {
            this.PositionHandle = this.GetAttribLocation("inPosition");
            this.ViewHandle = this.GetUniformLocation("inViewMatrix");
            this.ProjectionHandle = this.GetUniformLocation("inProjMatrix");
            this.WorldHandle = this.GetUniformLocation("inWorldMatrix");
        }
    }
}
