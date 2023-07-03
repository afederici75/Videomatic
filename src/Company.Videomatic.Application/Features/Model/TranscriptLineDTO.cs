namespace Company.Videomatic.Application.Features.Model;

public record TranscriptLineDTO(
    string Text = "",
    TimeSpan StartsAt = default,
    TimeSpan Duration = default);
