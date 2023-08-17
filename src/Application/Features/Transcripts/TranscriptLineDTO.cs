namespace Application.Features.Transcripts;

public class TranscriptLineDTO(
    string Text,
    TimeSpan? StartsAt,
    TimeSpan? Duration)
{ 
    public string Text { get; } = Text;
    public TimeSpan? StartsAt { get; } = StartsAt;
    public TimeSpan? Duration { get; } = Duration;

    private TranscriptLineDTO() : this("", default, default) 
    { }
}