namespace Company.Videomatic.Domain.Aggregates.Transcript;

public class Transcript : IAggregateRoot
{
    public static Transcript Create(VideoId videoId, string language)
    {
        var t = new Transcript
        {
            VideoId = videoId,
            Language = language,
        };        

        return t 

    }
    public static Transcript Create(VideoId videoId, string language, IEnumerable<string> lines)
    {
        return new Transcript
        {
            VideoId = videoId,
            Language = language,
        }.AddLines(lines);
    }

    public TranscriptId Id { get; private set; } = default!;
    public VideoId VideoId { get; private set; } = default!;
    public string Language { get; private set; } = default!;

    public IReadOnlyCollection<TranscriptLine> Lines
    {
        get => _lines.ToList();
        //private set => _lines = value.ToList();
    }

    public Transcript AddLines(IEnumerable<TranscriptLine> allLines)
    {
        _lines.AddRange(allLines);
        return this;
    }

    //public Transcript AddLines(IEnumerable<string> allText)
    //{
    //    var lines = allText.Select(t => (TranscriptLine)t).ToArray();
    //    _lines.AddRange(lines);
    //    return this;
    //}

    public Transcript AddLine(string text, TimeSpan duration, TimeSpan startsAt)
    {
        var line = TranscriptLine.Create(text, duration, startsAt);
        _lines.Add(line);

        return this;
    }

    #region Private

    private Transcript()
    { }

    private List<TranscriptLine> _lines { get; set; } = new();    

    #endregion
}

