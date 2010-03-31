namespace SampleApp
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Beerdriven.Mobile.Gaming;
    using Beerdriven.Mobile.Graphics.Egl;
    using Beerdriven.Mobile.Graphics.Egl.Interop;
    using Beerdriven.Mobile.Graphics.ES20;
    using Beerdriven.Mobile.Graphics.ES20.Interop;
    using Beerdriven.Mobile.Interop;
    using OpenTK;

    public class SampleGame : Game
    {
        private int exitCounter;

        protected glBuffer IndiceBuffer
        {
            get;
            private set;
        }

        protected SampleShader Shader
        {
            get;
            private set;
        }

        protected glBuffer VertexBuffer
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
                this.Shader.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void OnConfigureAttributes(eglAttribList attribs)
        {
            attribs.Add(NativeEgl.EGL_RED_SIZE, 5);
            attribs.Add(NativeEgl.EGL_GREEN_SIZE, 6);
            attribs.Add(NativeEgl.EGL_BLUE_SIZE, 5);
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
                var vertexShader = glShader.CompileFromFile(vertexShaderFile, NativeGl.GL_VERTEX_SHADER);

                var fragmentShaderFile = Path.Combine(currentDirectory, "FragmentShader.txt");
                var fragmentShader = glShader.CompileFromFile(fragmentShaderFile, NativeGl.GL_FRAGMENT_SHADER);

                // attach to program
                this.Shader = new SampleShader();
                this.Shader.AttachShader(vertexShader);
                this.Shader.AttachShader(fragmentShader);

                // link shader program
                if (!this.Shader.Link())
                {
                    var errorMessage = this.Shader.GetInfoLog();
                    this.Shader.Dispose();

                    throw new InvalidOperationException(string.Format("Failed to link program.\n {0}", errorMessage));
                }

                // create buffer's to contain our quad
                this.VertexBuffer = new glBuffer(NativeGl.GL_ARRAY_BUFFER);
                this.IndiceBuffer = new glBuffer(NativeGl.GL_ELEMENT_ARRAY_BUFFER);

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
                this.VertexBuffer.Bind();
                this.VertexBuffer.BufferData(
                        quadVertices.Length * Marshal.SizeOf(typeof(Vertex)), quadVertices, NativeGl.GL_STATIC_DRAW);

                uint[] indices = new uint[]
                                     {
                                             0, 1, 2, 3
                                     };

                this.IndiceBuffer.Bind();
                this.IndiceBuffer.BufferData(Marshal.SizeOf(typeof(uint)) * 4, indices, NativeGl.GL_STATIC_DRAW);

                // enable vertex attrib pointers
                NativeGl.glVertexAttribPointer(
                        this.Shader.PositionHandle,
                        3,
                        NativeGl.GL_FLOAT,
                        NativeGl.GL_FALSE,
                        Marshal.SizeOf(typeof(Vertex)),
                        IntPtr.Zero);

                NativeGl.glEnableVertexAttribArray(this.Shader.PositionHandle);

                // use the shader
                this.Shader.Use();

                // our camera matrix
                this.View = Matrix4.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0);

                // projection matrix
                var fovy = this.GraphicsContext.Width / (float)this.GraphicsContext.Height;
                this.Projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), fovy, 1, 100);

                // world matrix
                this.World = Matrix4.Identity;

                // TODO : Clean the matrix stuff
                unsafe
                {
                    fixed (float* matrixPtr = &this.View.Row0.X)
                    {
                        NativeGl.glUniformMatrix4fv(this.Shader.ViewHandle, 1, 0, matrixPtr);
                    }

                    fixed (float* matrixPtr = &this.Projection.Row0.X)
                    {
                        NativeGl.glUniformMatrix4fv(this.Shader.ProjectionHandle, 1, 0, matrixPtr);
                    }

                    fixed (float* matrixPtr = &this.World.Row0.X)
                    {
                        NativeGl.glUniformMatrix4fv(this.Shader.WorldHandle, 1, 0, matrixPtr);
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        protected override void OnRender(double deltaTime)
        {
            unsafe
            {
                fixed (float* matrixPtr = &this.World.Row0.X)
                {
                    NativeGl.glUniformMatrix4fv(this.Shader.WorldHandle, 1, 0, matrixPtr);
                }
            }

            NativeGl.glClear(NativeGl.GL_COLOR_BUFFER_BIT);
            NativeGl.glDrawElements(NativeGl.GL_TRIANGLE_STRIP, 4, NativeGl.GL_UNSIGNED_INT, IntPtr.Zero);
        }

        private float rotationAngle;

        protected override void OnUpdate(double deltaTime)
        {
            rotationAngle += (float)deltaTime * 0.03f;

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