
public class Disc
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
