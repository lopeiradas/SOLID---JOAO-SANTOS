
public class USB
{
    private string[] Files { get; set; }
    public int NumberOfFiles { get; }

    public USB(string[] files)
    {
        Files = files;
    }
 
    public string NombreFichero(int file) => Files[file];
}

