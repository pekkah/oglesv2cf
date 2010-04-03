namespace Beerdriven.Mobile.Graphics
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Egl;

    public interface IDeviceResourceManager
    {
        WindowSurface CreateWindowSurface(Config config, Form window);

        void Terminate();

        IEnumerable<Config> ChooseConfigs(AttribList attribList, int numberOfConfigsToReturn);

        RenderingContext CreateContext(Config config, AttribList attribList);

        void BindApi(uint api);
    }
}