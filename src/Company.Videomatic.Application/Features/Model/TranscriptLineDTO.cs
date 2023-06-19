namespace Company.Videomatic.Application.Features.Model;

public record TranscriptLineDTO(
    long Id = 0,
    long TranscriptId = 0,
    string Text = "",
    TimeSpan StartsAt = default,
    TimeSpan Duration = default);
