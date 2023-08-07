using Company.Videomatic.Domain.Aggregates.Video;

namespace Company.Videomatic.Domain.Aggregates.Transcript;

public class Transcript : IEntity, IAggregateRootx
{
    public static Transcript Create(VideoId videoId, string language)
    {
        var t = new Transcript
        {
            VideoId = videoId,
            Language = language,
        };

        return t;

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
        private set => _lines = value.ToList(); // TODO: code smell. For entity auto mapper when we convert string to TranscriptLine
    }
        

    public Transcript AddLines(IEnumerable<string> allText)
    {
        var lines = allText.Select(t => (TranscriptLine)t).ToArray();
        _lines.AddRange(lines);
        return this;
    }

    public Transcript AddLine(string text, TimeSpan? duration = null, TimeSpan? startsAt = null)
    {
        var line = TranscriptLine.Create(text, duration, startsAt);
        _lines.Add(line);

        return this;
    }

    #region Private

    private Transcript()
    { }



    List<TranscriptLine> _lines = new();

    int IEntity.Id => this.Id;

    #endregion
}

