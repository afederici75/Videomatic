namespace Company.Videomatic.Infrastructure.Data.Model;

public class Transcript : EntityBase
{
    public string Language { get; set; } = default!;

    public List<TranscriptLine> Lines { get; } = new();
}

