namespace Company.Videomatic.Application.Features.Videos;

public record ThumbnailDTO(
    long Id = 0,
    long VideoId = 0,
    string Location = "",
    ThumbnailResolutionDTO Resolution = ThumbnailResolutionDTO.Default,
    int Height = 0,
    int Width = 0);