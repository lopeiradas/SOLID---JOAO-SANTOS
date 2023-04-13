public class MultimediaDeviceBuilder : IMessageToDisplay
{
    private IMedia? CdPlayer { get; set; }
    private IMedia? DABRadio { get; set; }
    private IMedia? UsbPlayer { get; set; }
    private IMessageToDisplay MenuDeMedios { get; }

    public string MessageToDisplay => "Teste";

    public MultimediaDeviceBuilder(IMessageToDisplay menuDeMedios)
    {
        MenuDeMedios = menuDeMedios;

    }
    public class DABRadioCDException : MediaException
    {
        public DABRadioCDException(string message) : base(message)
        {
        }
    }

    public MultimediaDeviceBuilder SetMedia(CDPlayer media)
    {
        CdPlayer = media;
        return this;
    }
    public MultimediaDeviceBuilder SetMedia(USBPlayer media)
    {
        UsbPlayer = media;
        return this;
    }
    public MultimediaDeviceBuilder SetMedia(DABRadio media)
    {
        DABRadio = media;
        return this;
    }

    public MultimediaDevice Build()
    {
        IMedia cdPlayer = (IMedia)CdPlayer.Clone();
        IMedia usbPlayer = (IMedia)UsbPlayer.Clone();
        IMedia dabRadio = (IMedia)DABRadio.Clone();
        IMedia[] mediaI = { cdPlayer, usbPlayer, dabRadio };

        MultimediaDevice mD = new MultimediaDevice(mediaI, MenuDeMedios);
        return mD;
    }
}
