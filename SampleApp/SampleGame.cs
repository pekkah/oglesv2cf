namespace SampleApp
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Beerdriven.Mobile.Gaming;
    using Beerdriven.Mobile.Graphics.Egl;
    using Beerdriven.Mobile.Graphics.Egl.Enums;
    using Beerdriven.Mobile.Graphics.Egl.Interop;
    using Beerdriven.Mobile.Graphics.ES20;
    using Beerdriven.Mobile.Graphics.ES20.Interop;
    using Beerdriven.Mobile.Interop;
    using OpenTK;

    public class SampleGame : Game
    {
        private int exitCounter;

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

        protected DeviceBuffer VertexBuffer
        {
            get;
            private set;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
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
                var vertexShader = Shader.CompileFromFile(vertexShaderFile, NativeGl.GL_VERTEX_SHADER);

                var fragmentShaderFile = Path.Combine(currentDirectory, "FragmentShader.txt");
                var fragmentShader = Shader.CompileFromFile(fragmentShaderFile, NativeGl.GL_FRAGMENT_SHADER);

                // attach to program
                this.ShaderProgram = new SampleShader();
                this.ShaderProgram.AttachShader(vertexShader);
                this.ShaderProgram.AttachShader(fragmentShader);

                // link shader program
                if (!this.ShaderProgram.Link())
                {
                    var errorMessage = this.ShaderProgram.GetInfoLog();
                    this.ShaderProgram.Dispose();

                    throw new InvalidOperationException(string.Format("Failed to link program.\n {0}", errorMessage));
                }

                // create buffers to contain our quad
                this.VertexBuffer = this.GraphicsDevice.CreateBuffer(NativeGl.GL_ARRAY_BUFFER);
                this.IndiceBuffer = this.GraphicsDevice.CreateBuffer(NativeGl.GL_ELEMENT_ARRAY_BUFFER);

                Vertex[] quadVertices = new[]
                                            {
                                                    new Vertex
                                                        {
                                                                X = -0.5f,
                                                                Y = 0.5f,
                                                                Z = -5.0f
                                                        }, new Vertex
                                                               {
                                                                       X = -0.5f,
                                                                       Y = -0.5f,
                                                                       Z = -5.0f
                                                               }, new Vertex
                                                                      {
                                                                              X = 0.5f,
                                                                              Y = 0.5f,
                                                                              Z = -5.0f
                                                                      }, new Vertex
                                                                             {
                                                                                     X = 0.5f,
                                                                                     Y = -0.5f,
                                                                                     Z = -5
                                                                             }
                                            };

                // upload data to the buffers
                this.GraphicsDevice.BindBuffer(this.VertexBuffer);
                this.VertexBuffer.BufferData(
                        quadVertices.Length * Marshal.SizeOf(typeof(Vertex)), quadVertices, NativeGl.GL_STATIC_DRAW);

                uint[] indices = new uint[]
                                     {
                                             0, 1, 2, 3
                                     };

                this.GraphicsDevice.BindBuffer(this.IndiceBuffer);
                this.IndiceBuffer.BufferData(Marshal.SizeOf(typeof(uint)) * 4, indices, NativeGl.GL_STATIC_DRAW);

                // enable vertex attrib pointers
                this.GraphicsDevice.VertexAttribPointer(
                        this.ShaderProgram.PositionHandle,
                        3,
                        NativeGl.GL_FLOAT,
                        NativeGl.GL_FALSE,
                        Marshal.SizeOf(typeof(Vertex)));

                this.GraphicsDevice.EnabledVertexAttribArray(this.ShaderProgram.PositionHandle);

                // use the shader
                this.ShaderProgram.Use();

                // our camera matrix
                this.View = Matrix4.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0);

                // projection matrix
                var fovy = this.RenderingWindow.Width / (float)this.RenderingWindow.Height;
                this.Projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), fovy, 1, 100);

                // world matrix
                this.World = Matrix4.Identity;

                this.ShaderProgram.UniformMatrix4Fv(this.ShaderProgram.ViewHandle, 1, 0, this.View);
                this.ShaderProgram.UniformMatrix4Fv(this.ShaderProgram.ProjectionHandle, 1, 0, this.Projection);
                this.ShaderProgram.UniformMatrix4Fv(this.ShaderProgram.WorldHandle, 1, 0, this.World);

            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
                this.ExitGame = true;
            }
        }

        protected override void OnRender(double deltaTime)
        {
            this.ShaderProgram.UniformMatrix4Fv(this.ShaderProgram.WorldHandle, 1, 0, this.World);

            this.GraphicsDevice.Clear(NativeGl.GL_COLOR_BUFFER_BIT);

            this.GraphicsDevice.DrawElements(NativeGl.GL_TRIANGLE_STRIP, 4, NativeGl.GL_UNSIGNED_INT);
        }

        private float rotationAngle;

        protected override void OnUpdate(double deltaTime)
        {
            rotationAngle += (float)deltaTime * 0.01f;

            if (rotationAngle >= 360)
            {
                rotationAngle = 0;
            }

            this.World = Matrix4.CreateRotationZ(MathHelper.RadiansToDegrees(rotationAngle));

            // exit when screen pressed twice))
            if (this.exitCounter >= 2)
            {
                this.ExitGame = true;
            }
        }
    }
}