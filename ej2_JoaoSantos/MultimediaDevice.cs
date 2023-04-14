public class MultimediaDevice : IMedia
{
    private IMedia[] MediaDevices { get; set; }
    private IMedia? ActiveDevice { get; set; }
    private IMessageToDisplay? DevicesMenu { get; }
    public string Name => $"{ActiveDevice}";
    public string? MessageToDisplay
    {
        get
        {
            return $"MODO: {Name}\n" +
                   $"STATE: {ActiveDevice.MessageToDisplay}\n" +
                   $"{DevicesMenu.MessageToDisplay}";
        }
    }   

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
        IRemovableMedia<T> nuevaMedia = SetActiveDeviceToParameterizedMedia<T>();
        if (nuevaMedia.MediaIn) { throw new Exception("Ya hay un medio insertado"); }
        nuevaMedia.InsertMedia(media);
        nuevaMedia.Play();
    }
    public void Extract<T>()
    {
        IRemovableMedia<T> nuevaMedia = SetActiveDeviceToParameterizedMedia<T>();
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