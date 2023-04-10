public interface IMedia : IMessageToDisplay, ICloneable
{
    string Name { get; }
    void Play();
    void Stop();
    void Pause();
    void Previous();
    void Next();
}
