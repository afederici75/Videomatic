namespace Application.Features.Transcripts;

public record TranscriptLineDTO(
    string Text = "",
    TimeSpan? StartsAt = default,
    TimeSpan? Duration = default);
