namespace Beerdriven.Mobile.Graphics.ES20
{
    using System;
    using System.IO;
    using Enums;

    public class ShaderFactory
    {
        private readonly ShaderType shaderType;

        private string filename;

        public ShaderFactory(ShaderType shaderType)
        {
            this.shaderType = shaderType;
        }

        public ShaderFactory Source(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(string.Format("File '{0}' not found.", filename));
            }

            this.filename = filename;
            return this;
        }

        public Shader Compile()
        {
            Shader shader = new Shader(shaderType);

            var source = string.Empty;

            using (var reader = File.OpenText(filename))
            {
                source = reader.ReadToEnd();
            }

            shader.ShaderSource(source);

            if (!shader.Compile())
            {
                var infoLog = shader.GetInfoLog();

                shader.Dispose();

                throw new InvalidOperationException(string.Format("Failed to compile shader.\n {0}", infoLog));
            }

            return shader;
        }
    }
}