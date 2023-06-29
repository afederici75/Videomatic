namespace Company.Videomatic.Domain.Videos;

public class TranscriptLine : EntityBase
{
    internal static TranscriptLine Create(TranscriptId transcriptId, string text, TimeSpan duration, TimeSpan startsAt)
    {
        return new TranscriptLine
        {
            TranscriptId = transcriptId,
            Text = text,
            Duration = duration,
            StartsAt = startsAt
        };
    }

    public TranscriptId TranscriptId { get; private set; } = default!;
    public string Text { get; private set; } = default!;
    public TimeSpan Duration { get; private set; }
    public TimeSpan StartsAt { get; private set; }

    private TranscriptLine()
    { }

}

