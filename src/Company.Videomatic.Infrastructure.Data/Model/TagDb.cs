namespace Company.Videomatic.Infrastructure.Data.Model;

public class TagDb : EntityBaseDb
{
    public string Name { get; set; } = default!;

    public List<VideoDb> Videos { get; } = new();
    public List<VideoDbTagDb> VideoTags { get; } = new ();
}