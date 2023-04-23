using Newtonsoft.Json;

namespace Company.Videomatic.Domain;

public class Transcript
{
    public int Id { get; private set; }
    public Video Video { get; private set; }

    public string? Language { get; set; }

    public IReadOnlyList<TranscriptLine> Lines => _lines.AsReadOnly();
    
    private readonly List<TranscriptLine> _lines = new List<TranscriptLine>();


#pragma warning disable CS8618 
    [JsonConstructor]
    private Transcript()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }

    public Transcript(Video video, string? language = null)
    {        
        Video = video ?? throw new ArgumentNullException(nameof(video));
        Language = language;
    }

    public void SetVideo(Video video)
    {
        Video = video ?? throw new ArgumentNullException(nameof(video));
    }

    public Transcript AddLines(params TranscriptLine[] lines)
    {
        foreach (var line in lines)
        {
            line.SetTranscript(this);
        }

        _lines.AddRange(lines);

        return this;
    }

    public Transcript ClearLines()
    {
        _lines.Clear();
        return this;
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, Lines.Select(l => l.Text));
    }
}