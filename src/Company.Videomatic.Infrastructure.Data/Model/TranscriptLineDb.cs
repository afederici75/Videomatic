namespace Company.Videomatic.Infrastructure.Data.Model;

public class TranscriptLineDb : EntityBaseDb
{
    public long TranscriptId { get; set; }
    public string Text { get; set; } = default!;
    public TimeSpan Duration { get; set; }
    public TimeSpan StartsAt { get; set; }
}

