namespace Company.Videomatic.Infrastructure.Data.Model;

public class VideoDbTagDb 
{ 
    public long VideoId { get; set; }
    public long TagId { get; set; }

    public VideoDb Video { get; set; } = default!;
    public TagDb Tag { get; set; } = default!;
}