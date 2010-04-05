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

namespace Beerdriven.Mobile.Graphics.ES20
{
    using System;
    using System.Text;

    public class GraphicsDeviceException : Exception
    {
        public GraphicsDeviceException(string message) : base(message)
        {
        }

        public GraphicsDeviceException(string message, int errorCode) : base(message)
        {
            this.ErrorCode = errorCode;
            this.UpdateErrorId();
        }

        public GraphicsDeviceException(string formatMessage, int errorCode, params object[] args)
                : base(string.Format(formatMessage, args))
        {
            this.ErrorCode = errorCode;
            this.UpdateErrorId();
        }

        public int ErrorCode
        {
            get;
            private set;
        }

        public string ErrorId
        {
            get;
            private set;
        }

        public override string ToString()
        {
            var errorBuilder = new StringBuilder();
            errorBuilder.AppendFormat("GL ERROR CODE : '{0}'\n", this.ErrorCode.ToString("X"));
            errorBuilder.AppendFormat("GL ERROR CODE ID : '{0}'\n", this.ErrorId);
            errorBuilder.AppendLine(this.Message);

            return errorBuilder.ToString();
        }

        private void UpdateErrorId()
        {
            this.ErrorId = "NOT IMPLEMENTED"; // eglError.GetId(this.ErrorCode);
        }
    }
}