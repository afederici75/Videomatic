namespace Company.Videomatic.Domain.Video;

public class Video : ImportedEntity<VideoId>, IAggregateRoot
{
    public Video(string name, string? description = null)
        : base(name, description)
    {

    }

    //public Video(EntityOrigin origin)
    //    : base(origin)
    //{ }

    #region Private

    private Video() : base()
    { }

    #endregion
}
