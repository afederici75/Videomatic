namespace Company.Videomatic.Infrastructure.Data.Model;

public class VideoCollectionDb : EntityBaseDb
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public List<VideoDb> Videos { get; } = new();    
}

