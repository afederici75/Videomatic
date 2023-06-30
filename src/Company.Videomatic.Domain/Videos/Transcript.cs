namespace Company.Videomatic.Domain.Videos;

public record TranscriptId(long Value = 0)
{
    public static implicit operator long(TranscriptId x) => x.Value;
    //public static implicit operator TranscriptId(long x) => new TranscriptId(x);
}

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
        var line = TranscriptLine.Create(Id, text, duration, startsAt);
        _lines.Add(line);   

        return line;
    }

    #region Private

    private Transcript()
    { }

    private List<TranscriptLine> _lines { get; set; } = new();

    #endregion
}

