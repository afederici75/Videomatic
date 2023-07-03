namespace Company.Videomatic.Application.Features.Transcript;

public record TranscriptLineDTO(
    string Text = "",
    TimeSpan StartsAt = default,
    TimeSpan Duration = default);
