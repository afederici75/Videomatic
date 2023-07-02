namespace Company.Videomatic.Domain.Entities.TranscriptAggregate;

public class Transcript //: EntityBase
{
    internal static Transcript Create(VideoId videoId, string language)
    {
        return new Transcript
        {
            VideoId = videoId,
            Language = language
        };
    }

    public TranscriptId Id { get; private set; } = default!;
    public VideoId VideoId { get; private set; } = default!;
    public string Language { get; private set; } = default!;

    public IReadOnlyCollection<TranscriptLine> Lines
    {
        get => _lines.ToList();
        //private set => _lines = value.ToList();
    }

    public TranscriptLine AddLine(string text, TimeSpan duration, TimeSpan startsAt)
    {
        var line = TranscriptLine.Create(text, duration, startsAt);
        _lines.Add(line);

        return line;
    }

    #region Private

    private Transcript()
    { }

    private List<TranscriptLine> _lines { get; set; } = new();

    #endregion
}

