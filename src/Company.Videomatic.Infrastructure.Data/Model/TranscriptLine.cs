namespace Company.Videomatic.Infrastructure.Data.Model;

public class TranscriptLine : EntityBase
{
    public TranscriptLine(long transcriptId, string text, TimeSpan duration, TimeSpan startsAt)
    {
        TranscriptId = transcriptId;
        Text = text;
        Duration = duration;
        StartsAt = startsAt;
    }

    public long TranscriptId { get; private set; }
    public string Text { get; private set; } 
    public TimeSpan Duration { get; private set; }
    public TimeSpan StartsAt { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TranscriptLine()
    { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}

