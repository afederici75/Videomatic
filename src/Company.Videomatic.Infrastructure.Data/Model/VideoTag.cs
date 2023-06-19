namespace Company.Videomatic.Infrastructure.Data.Model;

public class VideoTag 
{ 
    public long VideoId { get; set; }
    public long TagId { get; set; }

    public Video Video { get; set; } = default!;
    public Tag Tag { get; set; } = default!;
}