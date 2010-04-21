#region license

// Copyright (c) 2010 Pekka Heikura
// 
//  Permission is hereby granted, free of charge, to any person
//  obtaining a copy of this software and associated documentation
//  files (the "Software"), to deal in the Software without
//  restriction, including without limitation the rights to use,
//  copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the
//  Software is furnished to do so, subject to the following
//  conditions:
// 
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
// 
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//  OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//  WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//  OTHER DEALINGS IN THE SOFTWARE.
// 

#endregion

namespace Beerdriven.Mobile.Graphics._3D.Modelling
{
    using OpenTK;

    public class Camera
    {
        public Vector3 Pos;

        public Vector3 Right;

        public Vector3 Target;

        public Vector3 Up;

        public Matrix4 View;

        public Camera(Vector3 pos, Vector3 target, Vector3 up)
        {
            this.Pos = pos;
            this.Target = target;
            this.View = Matrix4.LookAt(pos, target, up);

            // Even if the up vector is off, we can get
            // a reliable right vector
            //Right = Vector3.Cross(Target - Pos, Up);
            //Right.Normalize();
            // Given a correct right vector, can recalculate up
            //this.Up = Vector3.Cross(Right, Target - Pos);
            //this.Up.Normalize();   

            // Shortcut to get the Up and Right vectors
            this.Right.X = this.View.M11;
            this.Right.Y = this.View.M21;
            this.Right.Z = this.View.M31;
            this.Up.X = this.View.M12;
            this.Up.Y = this.View.M22;
            this.Up.Z = this.View.M32;
        }

        public static Quaternion CreateFromYawPitchRoll(
                Vector3 up, float yaw, Vector3 right, float pitch, Vector3 forward, float roll)
        {
            // Create a quaternion for each rotation, and multiply them 
            // together.  We normalize them to avoid using the conjugate
            Quaternion qyaw = new Quaternion(up, yaw);
            qyaw.Normalize();

            Quaternion qtilt = new Quaternion(right, pitch);
            qtilt.Normalize();

            Quaternion qroll = new Quaternion(forward, roll);
            qroll.Normalize();

            Quaternion yawpitch = qyaw * qtilt * qroll;
            yawpitch.Normalize();

            return yawpitch;
        }

        public static Matrix4 GetViewMatrix(
                ref Vector3 position, ref Vector3 target, ref Vector3 up, float yaw, float pitch, float roll)
        {
            // The right vector can be inferred
            Vector3 forward = target - position;
            Vector3 right = Vector3.Cross(forward, up);

            // This quaternion is the total of all the 
            // specified rotations
            Quaternion yawpitch = CreateFromYawPitchRoll(up, yaw, right, pitch, forward, roll);

            // Calculate the new target position, and the 
            // new up vector by transforming the quaternion
            target = position + Vector3.Transform(forward, yawpitch);
            up = Vector3.Transform(up, yawpitch);

            return Matrix4.LookAt(position, target, up);
        }

        public void Rotate(float pan, float tilt, float roll)
        {
            this.View = GetViewMatrix(ref this.Pos, ref this.Target, ref this.Up, pan, tilt, roll);

            this.Right = Vector3.Cross(this.Target - this.Pos, this.Up);
            this.Right.Normalize();
            this.Up = Vector3.Cross(this.Right, this.Target - this.Pos);
            this.Up.Normalize();
        }

        public void Translate(float forward, float right, float up)
        {
            // Move the camera position, and calculate a 
            // new target

            //Vector3 direction = Target - Pos;
            //direction.Normalize();
            // Shortcut to pull the above vector from the view matrix
            Vector3 direction = new Vector3(-this.View.M13, -this.View.M23, -this.View.M33);

            this.Pos += direction * forward;
            this.Pos += this.Right * right;
            this.Pos += this.Up * up;
            this.Target = this.Pos + direction;

            // Calculate the new view matrix
            this.View = Matrix4.LookAt(this.Pos, this.Target, this.Up);
        }
    }
}