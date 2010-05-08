namespace SampleApp
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Beerdriven.Mobile.Gaming;
    using Beerdriven.Mobile.Graphics._3D.Modelling;
    using Beerdriven.Mobile.Graphics.Egl;
    using Beerdriven.Mobile.Graphics.Egl.Enums;
    using Beerdriven.Mobile.Graphics.ES20;
    using Beerdriven.Mobile.Graphics.ES20.Enums;
    using Beerdriven.Mobile.Graphics.ES20.Interop;
    using Beerdriven.Mobile.Interop;
    using OpenTK;

    public class SampleGame : Game
    {
        private int exitCounter;

        private float rotationAngle;

        private Texture texture;

        protected DeviceBuffer IndiceBuffer
        {
            get;
            private set;
        }

        protected SampleShader ShaderProgram
        {
            get;
            private set;
        }

        protected TextureFactory TextureFactory
        {
            get;
            private set;
        }

        protected DeviceBuffer VertexBuffer
        {
            get;
            private set;
        }

        protected Camera Camera;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.texture.Dispose();
                this.VertexBuffer.Dispose();
                this.IndiceBuffer.Dispose();
                this.ShaderProgram.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void OnConfigureAttributes(Attribs<ConfigAttributes> attribs)
        {
            attribs.Add(ConfigAttributes.EGL_RED_SIZE, 5);
            attribs.Add(ConfigAttributes.EGL_GREEN_SIZE, 6);
            attribs.Add(ConfigAttributes.EGL_BLUE_SIZE, 5);
            attribs.Add(ConfigAttributes.EGL_DEPTH_SIZE, 16);
            attribs.AddEnd();
        }

        protected override void OnHandleMessage(NativeMessage windowsMessage)
        {
            base.OnHandleMessage(windowsMessage);

            if (windowsMessage.message == NativeWinuser.WM_LBUTTONDOWN)
            {
                this.exitCounter++;
            }
        }

        protected override void OnLoadContent()
        {
            try
            {
                var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

                // load shaders
                var vertexShaderFile = Path.Combine(currentDirectory, "VertexShader.txt");
                var fragmentShaderFile = Path.Combine(currentDirectory, "FragmentShader.txt");

                this.ShaderProgram = new ShaderProgramFactory<SampleShader>()
                                                .Shader(() => new ShaderFactory(ShaderType.GL_VERTEX_SHADER).Source(vertexShaderFile).Compile())
                                                .Shader(() => new ShaderFactory(ShaderType.GL_FRAGMENT_SHADER).Source(fragmentShaderFile).Compile()
                                   ).Link();

                // create buffers to contain our quad
                var bufferFactory = new BufferFactory();
                this.VertexBuffer = bufferFactory.CreateBuffer(BufferTarget.GL_ARRAY_BUFFER);
                this.IndiceBuffer = bufferFactory.CreateBuffer(BufferTarget.GL_ELEMENT_ARRAY_BUFFER);

                Vertex[] box = new[]
                                   {
                                           new Vertex(-0.5f, -0.5f, 0.5f, 0, 0), new Vertex(0.5f, -0.5f, 0.5f, 1f, 0),
                                           new Vertex(-0.5f, 0.5f, 0.5f, 0, 1f), new Vertex(0.5f, 0.5f, 0.5f, 1f, 1f),
                                           // BACK
                                           new Vertex(-0.5f, -0.5f, -0.5f, 1, 0), new Vertex(-0.5f, 0.5f, -0.5f, 1, 1),
                                           new Vertex(0.5f, -0.5f, -0.5f, 0, 0), new Vertex(0.5f, 0.5f, -0.5f, 0, 1),
                                           // LEFT
                                           new Vertex(-0.5f, -0.5f, 0.5f, 1, 0), new Vertex(-0.5f, 0.5f, 0.5f, 1, 1),
                                           new Vertex(-0.5f, -0.5f, -0.5f, 0, 0), new Vertex(-0.5f, 0.5f, -0.5f, 0, 1),
                                           // RIGHT
                                           new Vertex(0.5f, -0.5f, -0.5f, 1, 0), new Vertex(0.5f, 0.5f, -0.5f, 1, 1),
                                           new Vertex(0.5f, -0.5f, 0.5f, 0, 0), new Vertex(0.5f, 0.5f, 0.5f, 0, 1),
                                           // TOP
                                           new Vertex(-0.5f, 0.5f, 0.5f, 0, 0), new Vertex(0.5f, 0.5f, 0.5f, 1, 0),
                                           new Vertex(-0.5f, 0.5f, -0.5f, 0, 1), new Vertex(0.5f, 0.5f, -0.5f, 1, 1),
                                           // BOTTOM
                                           new Vertex(-0.5f, -0.5f, 0.5f, 1, 0), new Vertex(-0.5f, -0.5f, -0.5f, 1, 1),
                                           new Vertex(0.5f, -0.5f, 0.5f, 0, 0), new Vertex(0.5f, -0.5f, -0.5f, 0, 1)
                                   };

                // upload data to the buffers
                this.GraphicsDevice.BindBuffer(this.VertexBuffer);
                this.VertexBuffer.BufferData(box.Length * Marshal.SizeOf(typeof(Vertex)), box, BufferUsage.GL_STATIC_DRAW);

                //uint[] indices = new uint[]
                //                     {
                //                             0, 1, 2, 3
                //                     };

                //this.GraphicsDevice.BindBuffer(this.IndiceBuffer);
                //this.IndiceBuffer.BufferData(Marshal.SizeOf(typeof(uint)) * 4, indices, NativeGl.GL_STATIC_DRAW);

                // enable vertex attrib pointers
                this.GraphicsDevice.EnableVertexAttribArray(this.ShaderProgram.Vertex.Location);
                this.GraphicsDevice.EnableVertexAttribArray(this.ShaderProgram.TexCoord.Location);

                this.GraphicsDevice.VertexAttribPointer(
                        this.ShaderProgram.Vertex.Location,
                        3,
                        GlType.GL_FLOAT,
                        GlBoolean.GL_FALSE,
                        Marshal.SizeOf(typeof(Vertex)));

                this.GraphicsDevice.VertexAttribPointer(
                        this.ShaderProgram.TexCoord.Location,
                        2,
                        GlType.GL_FLOAT,
                        GlBoolean.GL_FALSE,
                        Marshal.SizeOf(typeof(Vertex)),
                        new IntPtr(Marshal.SizeOf(typeof(float)) * 3));

                // use the shader);));
                this.GraphicsDevice.UseProgram(this.ShaderProgram);

                // our camera matrix);)
                // this.View = Matrix4.LookAt(0, 0, 5, 0, 0, 0, 0, 1, 0);
                this.Camera = new Camera(new Vector3(-4, 1.5f, -4), new Vector3(0, 0, 0), new Vector3(0, 1, 0));

                this.Camera.Rotate(0, 0, 0);

                // projection matrix
                var fovy = this.RenderingWindow.Width / (float)this.RenderingWindow.Height;
                this.Projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), fovy, 1, 100);

                // world matrix
                this.World = Matrix4.CreateTranslation(0, -1, 0);

                // load texture
                this.TextureFactory = new TextureFactory(this.GraphicsDevice);
                this.texture = this.TextureFactory.CreateFromFile(Path.Combine(currentDirectory, @"Content\Textures\texture01.jpg"));

                this.GraphicsDevice.ActivateTextureUnit(TextureUnit.GL_TEXTURE1);
                this.GraphicsDevice.BindTexture(TextureTarget.GL_TEXTURE_2D, this.texture.TextureName);

                // set initial shader values
                this.ShaderProgram.BaseTexture.SetValue(this.texture, 1); // TextureUnit.GL_TEXTURE1
                this.ShaderProgram.World.SetValue(this.World);
                this.ShaderProgram.View.SetValue(this.Camera.View);
                this.ShaderProgram.Projection.SetValue(this.Projection);

                this.GraphicsDevice.Enable(Cap.GL_CULL_FACE);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
                this.ExitGame = true;
            }
        }

        protected override void OnRender(double deltaTime)
        {
            this.GraphicsDevice.Clear(ClearMask.GL_COLOR_BUFFER_BIT | ClearMask.GL_DEPTH_BUFFER_BIT);

            // this.GraphicsDevice.DrawElements(NativeGl.GL_TRIANGLE_STRIP, 4, NativeGl.GL_UNSIGNED_INT);
            this.GraphicsDevice.DrawArrays(PrimitiveType.GL_TRIANGLE_STRIP, 0, 4);
            this.GraphicsDevice.DrawArrays(PrimitiveType.GL_TRIANGLE_STRIP, 4, 4);
            this.GraphicsDevice.DrawArrays(PrimitiveType.GL_TRIANGLE_STRIP, 8, 4);
            this.GraphicsDevice.DrawArrays(PrimitiveType.GL_TRIANGLE_STRIP, 12, 4);
            this.GraphicsDevice.DrawArrays(PrimitiveType.GL_TRIANGLE_STRIP, 16, 4);
            this.GraphicsDevice.DrawArrays(PrimitiveType.GL_TRIANGLE_STRIP, 20, 4);
        }

        protected override void OnUpdate(double deltaTime)
        {
            this.rotationAngle += (float)deltaTime * 0.01f;

            if (this.rotationAngle >= 360)
            {
                this.rotationAngle = 0;
            }

            var rotationAngleRad = MathHelper.RadiansToDegrees(this.rotationAngle);

            this.World = Matrix4.CreateRotationZ(rotationAngleRad) * Matrix4.CreateTranslation(0, -1f, 0);

            this.ShaderProgram.World.SetValue(this.World);

            // exit when screen pressed twice)));
            if (this.exitCounter >= 2)
            {
                this.ExitGame = true;
            }
        }
    }
}