namespace Company.Videomatic.Domain.Videos;

public class TranscriptLine : ValueObject//: EntityBase
{
    internal static TranscriptLine Create(string text, TimeSpan duration, TimeSpan startsAt)
    {
        return new TranscriptLine
        {
            Text = text,
            Duration = duration,
            StartsAt = startsAt
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text;
        yield return Duration;
        yield return StartsAt;
    }

    public string Text { get; private set; } = default!;
    public TimeSpan Duration { get; private set; }
    public TimeSpan StartsAt { get; private set; }

    private TranscriptLine()
    { }

}

