namespace Company.Videomatic.Infrastructure.Data.Model;

public class TranscriptLine : EntityBase
{
    internal static TranscriptLine Create(long transcriptId, string text, TimeSpan duration, TimeSpan startsAt)
    {
        return new TranscriptLine
        {
            TranscriptId = transcriptId,
            Text = text,
            Duration = duration,
            StartsAt = startsAt
        };
    }

    public long TranscriptId { get; private set; }
    public string Text { get; private set; } = default!;
    public TimeSpan Duration { get; private set; }
    public TimeSpan StartsAt { get; private set; }

    private TranscriptLine()
    { }

}

