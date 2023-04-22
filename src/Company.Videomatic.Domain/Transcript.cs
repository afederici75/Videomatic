namespace Company.Videomatic.Domain;

public class Transcript
{ 
    public IEnumerable<TranscriptLine> Lines { get; set; } = Array.Empty<TranscriptLine>();
}