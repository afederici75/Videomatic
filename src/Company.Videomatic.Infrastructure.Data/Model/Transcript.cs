using System.Collections.Immutable;

namespace Company.Videomatic.Infrastructure.Data.Model;

public class Transcript : EntityBase
{
    public Transcript(long videoId, string language)
    {
        VideoId = videoId;
        Language = language;
    }   

    public long VideoId { get; private set; }   
    public string Language { get; private set; }

    public IReadOnlyCollection<TranscriptLine> Lines 
    { 
        get => _lines.ToList(); 
        //private set => _lines = value.ToList(); 
    }

    public Transcript AddLine(TranscriptLine line)
    { 
        _lines.Add(line);   
        return this;
    }

    #region Private

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Transcript()
    { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private List<TranscriptLine> _lines { get; set; } = new();

    #endregion
}

