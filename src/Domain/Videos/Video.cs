namespace Domain.Videos;

public class Video : ImportedEntity, IAggregateRoot
{
    public Video(string name, string? description = null)
        : base(name, description)
    {

    }

    public Video(EntityOrigin origin)
        : base(origin)
    { }

    public VideoId Id { get; private set; } = default!;

    #region Private

    private Video() : base()
    { }

    #endregion
}
