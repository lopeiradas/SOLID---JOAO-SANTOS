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

public class MultimediaDeviceBuilder
{
    private IMedia CdPlayer { get; set; }
    private IMedia DABRadio { get; set; }
    private IMedia UsbPlayer { get; set; }
    private IMessageToDisplay MenuDeMedios { get;}

    public MultimediaDeviceBuilder(IMessageToDisplay menuDeMedios)
    {
        MenuDeMedios = menuDeMedios;
    }

    public MultimediaDeviceBuilder SetMedia(CDPlayer media)
    {
        CdPlayer = media;
        return this;        
    }

    public MultimediaDeviceBuilder SetMedia(DABRadio media)
    {
        DABRadio = media;
        return this;
    }

    public MultimediaDeviceBuilder SetMedia(USBPlayer media)
    {
        UsbPlayer = media;
        return this;
    }

    public MultiMmdiaDevice Build()
    {
        return new MultimediaDevice(CdPlayer, DABRadio, UsbPlayer, MenuDeMedios);
    }
}


