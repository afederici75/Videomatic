namespace Company.Videomatic.Domain;

public class TranscriptLine
{
    public int Id { get; set; }
    public int TranscriptId { get; set; }

    public required string Text { get; set; }
    public TimeSpan? Duration { get; set; }    
    public TimeSpan? StartsAt { get; set; }

    public override string ToString()
    {
        return $"[StartsAt:{StartsAt?.Seconds}, Duration:{Duration?.TotalSeconds}] '{Text}'";
    }
}
