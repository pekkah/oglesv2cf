namespace Beerdriven.Mobile.Graphics
{
    using System;
    using System.Windows.Forms;
    using Egl;

    public interface IDisplayManager
    {
        Display GetDisplay(IntPtr deviceContext);

        Display GetDisplay(Form window);
    }
}