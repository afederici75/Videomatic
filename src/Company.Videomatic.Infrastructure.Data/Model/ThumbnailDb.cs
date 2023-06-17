namespace Company.Videomatic.Infrastructure.Data.Model;

public class ThumbnailDb : EntityBaseDb
{
    public string Location { get; set; } = default!;    
    public ThumbnailResolutionDb Resolution { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
}

