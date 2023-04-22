namespace Company.Videomatic.Domain;

public class VideoTranscriptItem
{
    public required string Text { get; set; }
    public TimeSpan? Duration { get; set; }    
    public TimeSpan StartsAt { get; set; }

    public override string ToString()
    {
        return $"[{StartsAt.Seconds}, {Duration?.TotalSeconds}]: '{Text}'";
    }
}
