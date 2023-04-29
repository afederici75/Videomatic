using Newtonsoft.Json;

namespace Company.Videomatic.Domain.Model;

public class Transcript : EntityBase
{
    public string? Language { get; set; }

    [JsonIgnore]
    public IEnumerable<TranscriptLine> Lines => _lines.AsReadOnly();

    [JsonProperty(PropertyName = nameof(Lines))]
    private List<TranscriptLine> _lines = new List<TranscriptLine>();


#pragma warning disable CS8618 
    [JsonConstructor]
    private Transcript()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }

    public Transcript(string? language = null)
    {
        Language = language;
        //_lines = lines?.ToList() ?? new List<TranscriptLine>();
    }

    public Transcript AddLines(params TranscriptLine[] lines)
    {
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