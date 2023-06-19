namespace Company.Videomatic.Infrastructure.Data.Model;

public class Artifact : EntityBase
{
    public long VideoId { get; set; }
    public string Title { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string? Text { get; set; }
}

