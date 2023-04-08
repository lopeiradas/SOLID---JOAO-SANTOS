




public class DABRadio : IMedia
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
