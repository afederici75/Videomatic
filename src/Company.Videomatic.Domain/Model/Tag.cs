namespace Company.Videomatic.Domain.Model;

public class Tag : EntityBase<string>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonConstructor]
    private Tag()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }

    [JsonIgnore]
    public IEnumerable<Video> Videos => _videos.AsReadOnly();

    [JsonProperty(PropertyName = nameof(Videos))]
    private List<Video> _videos = new List<Video>();
}
