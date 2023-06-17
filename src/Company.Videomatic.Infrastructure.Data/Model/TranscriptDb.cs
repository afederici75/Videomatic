namespace Company.Videomatic.Infrastructure.Data.Model;

public class TranscriptDb : EntityBaseDb
{
    public string Language { get; set; } = default!;

    public List<TranscriptLineDb> Lines { get; } = new();
}

