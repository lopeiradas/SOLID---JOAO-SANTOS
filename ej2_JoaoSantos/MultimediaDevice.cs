public class MultimediaDevice : IMedia
{
    private IMedia[] MediaDevices { get; set; }
    private IMedia? ActiveDevice { get; set; }
    private IMessageToDisplay? DevicesMenu { get; }
    public string? MessageToDisplay { get; }

    public string Name => ActiveDevice.Name;

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
        bool existe = false;
        foreach (var device in MediaDevices)
        {
            if (device is IRemovableMedia<T>)
            {
                ActiveDevice = device;
                existe = true;
                break;
            }
        }

        if (!existe) { throw new Exception("NO EXTRACT MEDIA FOUND..."); }
        return (IRemovableMedia<T>)ActiveDevice;
    }
    public void Insert<T>(T media)
    {
        if (ActiveDevice != null) { throw new Exception("Ya hay un medio insertado"); }

        var nuevaMedia = SetActiveDeviceToParameterizedMedia<T>();
        nuevaMedia.InsertMedia(ref media);
        nuevaMedia.Play();
    }
    public void Extract<T>()
    {
        var nuevaMedia = SetActiveDeviceToParameterizedMedia<T>();
        nuevaMedia.RemoveMedia();
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

    public void Play()
    {
        ActiveDevice.Play();
    }

    public void Stop()
    {
        ActiveDevice.Stop();
    }

    public void Pause()
    {
        ActiveDevice.Pause();
    }

    public void Previous()
    {
        ActiveDevice.Previous();
    }

    public void Next()
    {
        ActiveDevice.Next();
    }

    public object Clone()
    {
        return ActiveDevice.Clone();
    }
}