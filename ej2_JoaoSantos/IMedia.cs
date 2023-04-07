interface IMedia
{
    string MessageToDisplay { get; }
    void Play();
    void Stop();
    void Pause();
    void Previous();
    void Next();
}
