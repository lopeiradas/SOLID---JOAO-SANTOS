interface IRemovableMedia<T> : IMedia
{
    bool MediaIn { get; }
    void InsertMedia(ref T media);
    bool RemoveMedia();
}
