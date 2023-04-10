public class MultimediaDevice
{
    private IMedia[] MediaDevices { get; set; }
    private IMedia? ActiveDevice { get; set; }
    private IMessageToDisplay? DevicesMenu { get; }
    public string? MessageToDisplay { get; }

    public MultimediaDevice(IMedia[] medios, IMessageToDisplay devicesMenu)
    {
        if (medios.Length < 1)
        {
            throw new ArgumentException("Error tienes que colocar minimo 1 medio.");
        }
        MediaDevices = medios;
        DevicesMenu = devicesMenu;
        ActiveDevice = medios[0];
    }
    private IRemovableMedia<T> SetActiveDeviceToParameterizedMedia<T>()
    {

    }
    public void Insert<T>(T media)
    {

    }
    public void Extract<T>()
    {

    }
    public void SwitchMode()
    {
        if (ActiveDevice == MediaDevices[MediaDevices.Length - 1])
        {
            ActiveDevice = MediaDevices[0];
        }
        else
        {
            int i = Array.IndexOf(MediaDevices, ActiveDevice);
            ActiveDevice = MediaDevices[i + 1];
        }
    }
}