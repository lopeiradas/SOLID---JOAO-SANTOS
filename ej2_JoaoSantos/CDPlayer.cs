public class CDPlayer : IRemovableMedia<Disc>
{
    private Disc? Disc { get; set; }
    private ushort Track { get; set; }
    private MediaState State { get; set; }

    public class CDPlayerException : MediaException
    {
        public CDPlayerException(string message) : base(message)
        {
        }
    }

    public CDPlayer()
    {
        Disc = null;
        State = MediaState.Stopped;
    }

    public bool MediaIn => Disc != null;

    public void InsertMedia(Disc media)
    {
        Disc = media;
        State = MediaState.Stopped;
    }
    public bool RemoveMedia()
    {
        bool ok = MediaIn;
        Disc = null;
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
                        mensaje = $"STOPPED... {Disc}";
                        break;
                    case MediaState.Paused:
                        mensaje = $"PAUSED... {Disc}. Track {Track} - {Disc!.NombreCancion(Track - 1)}";
                        break;
                    case MediaState.Playing:
                        mensaje = $"PLAYING... {Disc}. Track {Track} - {Disc!.NombreCancion(Track - 1)}";
                        break;
                    default:
                        mensaje = "ERROR";
                        break;
                }
            }
            else
                mensaje = "NO DISC";
            return mensaje;
        }
    }

    public string Name => MessageToDisplay;

    public void Play()
    {
        if (State == MediaState.Stopped)
            Track = 1;
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
                throw new CDPlayerException("Estado de reproducción desconocido.");
        }
    }

    public void Next()
    {
        if (MediaIn && State != MediaState.Stopped)
        {
            Track = (ushort)((MediaIn && Track == Disc!.NumTracks) ? 1 : Track + 1);
            Play();
        }
    }

    public void Previous()
    {
        if (MediaIn && State != MediaState.Stopped)
        {
            Track = (ushort)((Track == 1) ? Disc!.NumTracks : Track - 1);
            Play();
        }
    }

    public void Stop() => State = MediaState.Stopped;

    public object Clone() => new CDPlayer();
}
