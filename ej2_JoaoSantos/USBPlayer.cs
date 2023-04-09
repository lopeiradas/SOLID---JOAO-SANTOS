
public class USBPlayer : IRemovableMedia<USB>
{
    private USB? Usb { get; set; }
    private ushort FileNumber { get; set; }
    private MediaState State { get; set; }

    public class USBPlayerException : MediaException
    {
        public USBPlayerException(string message) : base(message)
        {
        }
    }

    public USBPlayer()
    {
        Usb = null;
        State = MediaState.Stopped;
    }

    public bool MediaIn => Usb != null;

    public void InsertMedia(ref USB media)
    {
        Usb = media;
        State = MediaState.Stopped;
    }
    public bool RemoveMedia()
    {
        bool ok = MediaIn;
        Usb = null;
        State = MediaState.Stopped;
        return ok;
    }

    public string MessageToDisplay
    {
        get
        {
            string mensaje;

            if (MediaIn)
            {
                switch (State)
                {
                    case MediaState.Stopped:
                        mensaje = $"STOPPED... {Usb}";
                        break;
                    case MediaState.Paused:
                        mensaje = $"PAUSED... {Usb}. Track {FileNumber} - {Usb!.NombreFichero(FileNumber - 1)}";
                        break;
                    case MediaState.Playing:
                        mensaje = $"PLAYING... {Usb}. Track {FileNumber} - {Usb!.NombreFichero(FileNumber - 1)}";
                        break;
                    default:
                        mensaje = "ERROR";
                        break;
                }
            }
            else
                mensaje = "NO USB";
            return mensaje;
        }
    }

    public string Name => "USBPLAYER MODE";

    public void Play()
    {
        if (State == MediaState.Stopped)
            FileNumber = 1;
        State = MediaState.Playing;
    }

    public void Pause()
    {
        switch (State)
        {
            case MediaState.Stopped:
                break;
            case MediaState.Paused:
                State = MediaState.Playing;
                break;
            case MediaState.Playing:
                State = MediaState.Paused;
                break;
            default:
                throw new USBPlayerException("Estado de reproducción desconocido.");
        }
    }

    public void Next()
    {
        if (MediaIn && State != MediaState.Stopped)
        {
            FileNumber = (ushort)((MediaIn && FileNumber == Usb!.FileNumber) ? 1 : FileNumber + 1);
            Play();
        }
    }

    public void Previous()
    {
        if (MediaIn && State != MediaState.Stopped)
        {
            FileNumber = (ushort)((FileNumber == 1) ? Usb!.NumTracks : Track - 1);
            Play();
        }
    }

    public void Stop() => State = MediaState.Stopped;

    public object Clone() => new CDPlayer();

    public void InsertMedia(ref USB media)
    {
        throw new NotImplementedException();
    }
}

