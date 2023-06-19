namespace Company.Videomatic.Infrastructure.Data.Model;

public class VideoTag : EntityBase
{ 
    public long VideoId { get; set; }
    public string Name { get; set; } = default!;
}