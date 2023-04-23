namespace Company.Videomatic.Domain;

public class TranscriptLine
{
    public int Id { get; private set; }
    public Transcript Transcript { get; private set; }

    public string Text { get; set; }
    public TimeSpan? Duration { get; set; }    
    public TimeSpan? StartsAt { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TranscriptLine()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }

    public TranscriptLine(Transcript transcript, string text, TimeSpan? duration, TimeSpan? startsAt)
    {
        Transcript = transcript ?? throw new ArgumentNullException(nameof(transcript));
        Text = text ?? throw new ArgumentNullException(nameof(text));
        Duration = duration;
        StartsAt = startsAt;
    }

    public void SetTranscript(Transcript transcript)
    {
        Transcript = transcript ?? throw new ArgumentNullException(nameof(transcript));
    }     

    public override string ToString()
    {
        return $"[StartsAt:{StartsAt?.Seconds}, Duration:{Duration?.TotalSeconds}] '{Text}'";
    }
}
