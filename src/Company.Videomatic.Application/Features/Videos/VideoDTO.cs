namespace Company.Videomatic.Application.Features.Videos;

public record VideoDTO(
    long Id = 0,
    string Location = "",
    string Title = "",
    string? Description = default);

public record TranscriptDTO(
    long Id = 0,
    long VideoId = 0,
    string Language = "");

public record TranscriptLineDTO(
    long Id = 0,
    long TranscriptId = 0,
    string Text = "",
    TimeSpan StartsAt = default,
    TimeSpan Duration = default);

public enum ThumbnailResolutionDTO
{
    Default,
    Medium,
    High,
    Standard,
    MaxRes
}

public record ThumbnailDTO(
    long Id = 0,
    long VideoId = 0,
    string Location = "",
    ThumbnailResolutionDTO Resolution = ThumbnailResolutionDTO.Default,
    int Height = 0,
    int Width = 0);