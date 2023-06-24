namespace Company.Videomatic.Infrastructure.Data.Model;

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
    public string Title { get; private set; }
    public string Type { get; private set; }
    public string? Text { get; private set; }

    #region Private

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Artifact()
    { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion
}

