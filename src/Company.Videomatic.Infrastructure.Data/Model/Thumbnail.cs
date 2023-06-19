using Company.Videomatic.Application.Features.Model;

namespace Company.Videomatic.Infrastructure.Data.Model;

public class Thumbnail : EntityBase
{
    public long VideoId { get; set; }
    public string Location { get; set; } = default!;    
    public ThumbnailResolution Resolution { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
}

