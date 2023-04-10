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
