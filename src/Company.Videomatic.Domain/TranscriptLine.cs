namespace Company.Videomatic.Domain;

public class TranscriptLine
{
    public required string Text { get; set; }
    public TimeSpan? Timemark { get; set; }    
}
