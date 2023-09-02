using Domain.Videos;
using System.Web;

namespace Domain.Transcripts;

public class Transcript : TrackedEntity, IAggregateRoot
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

    public IList<TranscriptLine> Lines { get; private set; } = new List<TranscriptLine>();

    public Transcript AddLines(IEnumerable<string> allText)
    {
        var lines = allText.Select(t => (TranscriptLine)t).ToArray();
        foreach (var l in lines)
            Lines.Add(l);
        return this;
    }
    
    public Transcript AddLine(string text, TimeSpan? duration = null, TimeSpan? startsAt = null)
    {
        var line = new TranscriptLine(text, duration, startsAt);
        Lines.Add(line);
    
        return this;
    }

    public string GetFullText()
    {
        var text = HttpUtility.HtmlDecode(string.Join(" ", Lines.Select(l => l.Text)));

        return text;
    }

    #region Private

    private Transcript()
    { }

    #endregion
}
