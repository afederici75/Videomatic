namespace Company.Videomatic.Domain.Aggregates.Video;

public class VideoTag : ValueObject//:EntityBase
{
    public static implicit operator string(VideoTag x) => x.Name;
    public static implicit operator VideoTag(string x) => new (x);

    public VideoTag(string name)
    {
        Name = name;
    }

    public string Name { get; private set; } = default!;

    private VideoTag()
    { }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}