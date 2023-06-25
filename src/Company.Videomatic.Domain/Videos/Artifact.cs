namespace Company.Videomatic.Domain.Videos;

public class Artifact : EntityBase
{
    internal static Artifact Create(long videoId, string title, string type, string? text = null)
    {
        return new Artifact
        {
            VideoId = videoId,
            Title = title,
            Type = type,
            Text = text,
        };
    }

    public long VideoId { get; private set; }
    public string Title { get; private set; } = default!;
    public string Type { get; private set; } = default!;
    public string? Text { get; private set; }

    #region Private

    private Artifact()
    { }

    #endregion
}

