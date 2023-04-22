namespace Company.Videomatic.Domain;

public class VideoTranscript
{ 
    public IEnumerable<VideoTranscriptItem> Lines { get; set; } = Array.Empty<VideoTranscriptItem>();

    public override string ToString()
    {
        return string.Join(Environment.NewLine, Lines.Select(l => l.Text));
    }
}