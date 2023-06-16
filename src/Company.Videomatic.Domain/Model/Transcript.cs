namespace Company.Videomatic.Domain.Model;

public class Transcript : EntityBase
{
    public string Language { get; private set; }

    [JsonIgnore]
    public IEnumerable<TranscriptLine> Lines => _lines.AsReadOnly();

    public Transcript(string language)
    {
        Language = Guard.Against.NullOrWhiteSpace(language, nameof(language));
    }

    #region Methods

    public override string ToString()
    {
        return string.Join(Environment.NewLine, Lines.Select(l => l.Text));
    }

    public Transcript AddLine(TranscriptLine newLine)
    {
        _lines.Add(Guard.Against.Null(newLine, nameof(newLine)));

        return this;
    }

    public Transcript ClearLines()
    {
        _lines.Clear();

        return this;
    }

    #endregion

    #region Private

#pragma warning disable CS8618
    [JsonConstructor]
    private Transcript()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }

    [JsonProperty(PropertyName = nameof(Lines))]
    private List<TranscriptLine> _lines = new List<TranscriptLine>();

    #endregion
}