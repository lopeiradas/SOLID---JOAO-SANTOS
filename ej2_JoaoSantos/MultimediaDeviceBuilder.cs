public class MultimediaDeviceBuilder : IMessageToDisplay
{
    private IMedia? CdPlayer { get; set; }
    private IMedia? DABRadio { get; set; }
    private IMedia? UsbPlayer { get; set; }
    private IMessageToDisplay MenuDeMedios { get; }

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

    public DABRadioCD()
    {

        Cd = new CDPlayer();
        Radio = new DABRadio();
        ActiveDevice = Radio;
    }

}
