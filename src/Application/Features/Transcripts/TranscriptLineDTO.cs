namespace Application.Features.Transcripts;

public class TranscriptLineDTO(
    string Text = "",
    TimeSpan? StartsAt = default,
    TimeSpan? Duration = default)
{ 
    public string Text { get; } = Text;
    public TimeSpan? StartsAt { get; } = StartsAt;
    public TimeSpan? Duration { get; } = Duration;

}