namespace Beerdriven.Mobile.Graphics.ES20
{
    using Enums;

    public class BufferFactory
    {
        public DeviceBuffer CreateBuffer(BufferTarget target)
        {
            return new DeviceBuffer(target);
        }
    }
}