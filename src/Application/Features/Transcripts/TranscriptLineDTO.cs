namespace Application.Features.Transcripts;

public readonly record struct TranscriptLineDTO(
    string Text = "",
    TimeSpan? StartsAt = default,
    TimeSpan? Duration = default);
