namespace Company.Videomatic.Infrastructure.Data.Model;

public class ArtifactDb : EntityBaseDb
{
    public string Title { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string? Text { get; set; }
}

