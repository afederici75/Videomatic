namespace Company.Videomatic.Infrastructure.Data.Model;

public class Transcript : EntityBase
{
    public long VideoId { get; set; }   
    public string Language { get; set; } = default!;

    public List<TranscriptLine> Lines { get; } = new();
}

