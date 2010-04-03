namespace Beerdriven.Mobile.Graphics.Egl
{
    using System;
    using System.Windows.Forms;
    using Mobile.Interop;

    public class DisplayManager : IDisplayManager
    {
        public Display GetDisplay(IntPtr deviceContext)
        {
            var display = new Display(deviceContext);

            return display;
        }

        public Display GetDisplay(Form window)
        {
            var deviceContext = NativeCore.GetDC(window.Handle);

            return GetDisplay(deviceContext);
        }
    }
}