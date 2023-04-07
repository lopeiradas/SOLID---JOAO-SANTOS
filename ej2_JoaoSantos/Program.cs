
interface IMedia
{
    string MessageToDisplay { get; }
    void Play();
    void Stop();
    void Pause();
    void Previous();
    void Next();
}

class MediaException : Exception
{
    public MediaException(string message) : base(message)
    {
    }
}

enum MediaState
{
    Stopped,
    Paused,
    Playing
}

class Disc
{
    private string Album { get; set; }
    private string Artist { get; set; }
    private string[] Songs { get; set; }

    public Disc(string album, string artist, string[] songs)
    {
        Album = album;
        Artist = artist;
        Songs = songs;
    }

    public int NumTracks => Songs.Length;
    public string NombreCancion(int indice) => Songs[indice];
    public override string ToString() => $"Album: {Album} Artist: {Artist}";
}

class CDPlayer : IMedia
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

    public bool ExtractMedia()
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
}

class DABRadio : IMedia
{
    const float SEEK_STEP = 0.5f;
    const float MAX_FREQUENCY = 108f;
    const float MIN_FRECUENCY = 87.5f;

    public class DABRadioException : MediaException
    {
        public DABRadioException(string message) : base(message)
        {
        }
    }

    private float Frequency { get; set; }
    private MediaState State { get; set; }

    public DABRadio()
    {
        Frequency = MIN_FRECUENCY;
        State = MediaState.Stopped;
    }

    public string MessageToDisplay
    {
        get
        {
            string mensaje;

            switch (State)
            {
                case MediaState.Paused:
                    mensaje = $"PAUSED - BUFFERING... FM - {Frequency:F1} MHz";
                    break;
                case MediaState.Playing:
                    mensaje = $"HEARING... FM - {Frequency:F1} MHz";
                    break;
                case MediaState.Stopped:
                    mensaje = $"RADIO OFF";
                    break;
                default:
                    mensaje = "ERROR";
                    break;
            }
            return mensaje;
        }
    }

    public void Play() => State = MediaState.Playing;

    public void Stop() => State = MediaState.Stopped;

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
                throw new DABRadioException("Estado de reproducción desconocido.");
        }
    }

    public void Next()
    {
        if (State != MediaState.Stopped)
        {
            Frequency = (Frequency + SEEK_STEP > MAX_FREQUENCY) ? MIN_FRECUENCY : Frequency + SEEK_STEP;
            Play();
        }
    }

    public void Previous()
    {
        if (State != MediaState.Stopped)
        {
            Frequency = (Frequency - SEEK_STEP > MIN_FRECUENCY) ? MAX_FREQUENCY : Frequency - SEEK_STEP;
            Play();
        }
    }
}


class DABRadioCD
{
    private CDPlayer Cd { get; set; }
    private DABRadio Radio { get; set; }
    private IMedia ActiveDevice { get; set; }

    public class DABRadioCDException : MediaException
    {
        public DABRadioCDException(string message) : base(message)
        {
        }
    }

    public DABRadioCD()
    {
        Cd = new CDPlayer();
        Radio = new DABRadio();
        ActiveDevice = Radio;
    }

    public void Play() => ActiveDevice.Play();
    public void Stop() => ActiveDevice.Stop();
    public void Pause() => ActiveDevice.Pause();

    public void Next() => ActiveDevice.Next();

    public void Previous() => ActiveDevice.Next();

    public string MessageToDisplay
    {
        get
        {
            string modo = (ActiveDevice is DABRadio) ? "DAB" : "CD";
            return
                $"MODO: {modo}\n" +
                $"STATE: {ActiveDevice.MessageToDisplay}\n" +
                "[1]Play " +
                "[2]Pause " +
                "[3]Stop " +
                "[4]Prev " +
                "[5]Next " +
                "[6]Switch " +
                "[7]Insert CD " +
                "[8]Extract CD, " +
                "[ESC]Turn off";
        }
    }

    public Disc InsertCD
    {
        set
        {
            if (Cd.MediaIn)
                throw new DABRadioCDException("Hay ya un CD en el reproductor.");

            // LEY DE DEMETER
            Cd.InsertMedia(value);

            ActiveDevice = Cd;
            Play();
        }
    }

    public void ExtractCD()
    {
        Cd.ExtractMedia();
        ActiveDevice = Radio;
    }

    public void SwitchMode()
    {
        if (ActiveDevice is DABRadio)
        {
            ActiveDevice = Cd;
            Play();
        }
        else
            ActiveDevice = Radio;
    }
}

class Program
{
    public static void Main()
    {
        string[] canciones = {
                "Wanna Be Startin' Somethin",
                "Baby Be Mine",
                "The Girl Is Mine",
                "Thriller",
                "Beat It",
                "Billie Jean",
                "Human Nature",
                "P.Y.T. (Pretty Young Thing)",
                "The Lady in My Life"
            };
        Disc thriller = new Disc("Thriller", "Michael Jackson", canciones);
        DABRadioCD radioCD = new DABRadioCD();
        ConsoleKeyInfo tecla = new ConsoleKeyInfo();
        do
        {
            try
            {
                Console.WriteLine(radioCD.MessageToDisplay);
                tecla = Console.ReadKey(true);
                Console.Clear();

                switch (tecla.KeyChar)
                {
                    case '1':
                        radioCD.Play();
                        break;
                    case '2':
                        radioCD.Pause();
                        break;
                    case '3':
                        radioCD.Stop();
                        break;
                    case '4':
                        radioCD.Previous();
                        break;
                    case '5':
                        radioCD.Next();
                        break;
                    case '6':
                        radioCD.SwitchMode();
                        break;
                    case '7':
                        radioCD.InsertCD = thriller;
                        break;
                    case '8':
                        radioCD.ExtractCD();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        } while (tecla.Key != ConsoleKey.Escape);
    }
}
