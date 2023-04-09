class Program
{
    private class MenuDispositivoMultimedia : IMessageToDisplay
    {
        public string MessageToDisplay =>
                "[1]Play \n" + "[2]Pause \n" +
                "[3]Stop \n" + "[4]Prev \n" + "[5]Next \n" +
                "[6]Switch \n" + "[7]Insert CD \n" +
                "[8]Extract CD \n" + "[9]Insert USB \n" +
                "[10]Extract USB \n" + "[13]Turn off";
    }

    public static void Main()
    {
        string[] cancionesEnCD = {
                "Wanna Be Startin' Somethin",
                "Baby Be Mine", "The Girl Is Mine", "Thriller", "Beat It",
                "Billie Jean", "Human Nature",
                "P.Y.T. (Pretty Young Thing)", "The Lady in My Life"
            };
        string[] ficherosEnUSB = {
                "One After 909_Let It Be_The Beatles.mp3",
                "Rocker_Let It Be_The Beatles.mp3",
                "Save the Last Dance for Me_Let It Be_The Beatles.mp3",
                "Don't Let Me Down_Let It Be_The Beatles.mp3",
                "Dig a Pony_Let It Be_The Beatles.mp3",
                "I've Got a Feeling_Let It Be_The Beatles.mp3",
                "Get Back_Let It Be_The Beatles.mp3"
            };

        Disc CD_DeThriller = new("Thriller", "Michael Jackson", cancionesEnCD);
        USB USB_DeThriller = new(ficherosEnUSB);
        MultimediaDevice dispositivoMultimedia =
                            new MultimediaDeviceBuilder(new MenuDispositivoMultimedia())
                            .SetMedia(new CDPlayer())
                            .SetMedia(new DABRadio())
                            .SetMedia(new USBPlayer())
                            .Build();

        int opcion = int.MaxValue;
        do
        {
            try
            {
                Console.WriteLine(dispositivoMultimedia.MessageToDisplay);
                Console.Write("Escoge una opción: ");
                opcion = int.Parse(Console.ReadLine() ?? "11");
                Console.Clear();
                switch (opcion)
                {
                    case 1: dispositivoMultimedia.Play(); break;
                    case 2: dispositivoMultimedia.Pause(); break;
                    case 3: dispositivoMultimedia.Stop(); break;
                    case 4: dispositivoMultimedia.Previous(); break;
                    case 5: dispositivoMultimedia.Next(); break;
                    case 6: dispositivoMultimedia.SwitchMode(); break;
                    case 7: dispositivoMultimedia.Insert(CD_DeThriller); break;
                    case 8: dispositivoMultimedia.Extract<Disc>(); break;
                    case 9: dispositivoMultimedia.Insert(USB_DeThriller); break;
                    case 10: dispositivoMultimedia.Extract<USB>(); break;
                    default:
                        break;
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        } while (opcion > 0 && opcion < 11);
    }
}

public class MultimediaDeviceBuilder
{
    private IMedia CdPlayer { get; set; }
    private IMedia DABRadio { get; set; }
    private IMedia UsbPlayer { get; set; }
    private IMessageToDisplay MenuDeMedios { get; }

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
}

