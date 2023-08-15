using Domain.Videos;

namespace Domain.Transcripts;

public class Transcript : TrackableEntity, IAggregateRoot
{
    public Transcript(VideoId videoId, string language)
    {
        VideoId = videoId;
        Language = language;
    }

    public Transcript(VideoId videoId, string language, IEnumerable<string> lines)
    {
        VideoId = videoId;
        Language = language;
        AddLines(lines);
    }

    public TranscriptId Id { get; private set; } = default!;
    public VideoId VideoId { get; private set; } = default!;
    public string Language { get; private set; } = default!;

    public IEnumerable<TranscriptLine> Lines => _lines;                   

    public Transcript AddLines(IEnumerable<string> allText)
    {
        var lines = allText.Select(t => (TranscriptLine)t).ToArray();
        _lines.AddRange(lines);
        return this;
    }

    public Transcript AddLine(string text, TimeSpan? duration = null, TimeSpan? startsAt = null)
    {
        var line = new TranscriptLine(text, duration, startsAt);
        _lines.Add(line);

        return this;
    }

    #region Private

    private Transcript()
    { }

    readonly List<TranscriptLine> _lines = new();    

    #endregion
}
