namespace Company.Videomatic.Domain.Aggregates.Video;

public class VideoTag : ValueObject
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

    public override string ToString()
    {
        return $"VideoTag({Name})";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name.ToUpper(); // Case insensitive
    }
}