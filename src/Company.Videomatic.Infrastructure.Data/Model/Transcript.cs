using System.Collections.Immutable;

namespace Company.Videomatic.Infrastructure.Data.Model;

public class Transcript : EntityBase
{
    internal static Transcript Create(long videoId, string language)
    {
        return new Transcript
        {
            VideoId = videoId,
            Language = language
        };
    }   

    public long VideoId { get; private set; }   
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

