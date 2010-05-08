namespace Beerdriven.Mobile.Graphics.ES20
{
    using System;
    using System.Collections.Generic;

    public class ShaderProgramFactory<TShaderProgram> where TShaderProgram : ShaderProgram, new()
    {
        public ShaderProgramFactory()
        {
            this.shaderCreators = new List<Func<Shader>>(2);
        }

        List<Func<Shader>> shaderCreators;

        public ShaderProgramFactory<TShaderProgram> Shader(Func<Shader> createShader)
        {
            this.shaderCreators.Add(createShader);
            return this;
        }

        public TShaderProgram Link()
        {
            var program = new TShaderProgram();

            // create shaders
            var shaders = new List<Shader>();

            foreach (var createShader in shaderCreators)
            {
                var shader = createShader();

                if (shader == null)
                {
                    throw new InvalidOperationException("Shader creator func returned a null shader.");
                }

                shaders.Add(shader);
            }

            if (shaders.Count == 0)
            {
                throw new InvalidOperationException("Cannot link program. No shaders.");
            }

            // attach to program
            foreach (var shader in shaders)
            {
                program.AttachShader(shader);
            }

            if(!program.Link())
            {
                throw new GraphicsDeviceException("Could not link program '{0}'", -1, program.GetInfoLog());
            }

            return program;
        }
    }
}