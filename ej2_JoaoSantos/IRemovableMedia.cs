interface IRemovableMedia<T> : IMedia
{
    bool MediaIn { get; }
    void InsertMedia(T media);
    bool RemoveMedia();
}
