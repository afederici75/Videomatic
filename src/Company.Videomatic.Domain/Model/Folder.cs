using Newtonsoft.Json;

namespace Company.Videomatic.Domain.Model;

public class Folder : EntityBase
{
    public string Name { get; set; }

    public Folder? Parent { get; set; }

    [JsonIgnore]
    public IEnumerable<Video> Videos => _videos.AsReadOnly();

    [JsonIgnore]
    public IEnumerable<Folder> Children => _children.AsReadOnly();

    [JsonProperty(PropertyName = nameof(Videos))]
    private List<Video> _videos = new List<Video>();

    [JsonProperty(PropertyName = nameof(Children))]
    private List<Folder> _children = new List<Folder>();

    public Folder(string name, IEnumerable<Video>? videos = null, IEnumerable<Folder>? children = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        _videos = videos?.ToList() ?? _videos;
        _children = children?.ToList() ?? _children;
    }

    public Folder(Folder? parent, string name, IEnumerable<Video> videos, IEnumerable<Folder> children)
        : this(name, videos, children)
    {
        Parent = parent ?? throw new ArgumentNullException(nameof(parent));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonConstructor]
    private Folder()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }



}