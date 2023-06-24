using System.Xml.Linq;

namespace Company.Videomatic.Infrastructure.Data.Model;

public class VideoTag : EntityBase
{
    internal static VideoTag Create(long videoId, string name)
    {
        return new VideoTag()
        {
            VideoId = videoId,
            Name = name
    };
        
    }

    public long VideoId { get; private set; }
    public string Name { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private VideoTag()
    { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}