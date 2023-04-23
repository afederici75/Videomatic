using System.Collections.ObjectModel;

namespace Company.Videomatic.Domain;

public class Transcript
{
    public int Id { get; set; }
    public int VideoId { get; set; }

    public IList<TranscriptLine> Lines { get; set; } = new List<TranscriptLine>();

    public override string ToString()
    {
        return string.Join(Environment.NewLine, Lines.Select(l => l.Text));
    }
}