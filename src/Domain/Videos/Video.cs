namespace Domain.Videos;

public class Video : ImportedEntity, IAggregateRoot
{
    public Video(string name, string? description = null)
        : base(name, description)
    {

    }

    public VideoId Id { get; private set; } = default!;

    //public Video(EntityOrigin origin)
    //    : base(origin)
    //{ }

    #region Private

    private Video() : base()
    { }

    #endregion
}
