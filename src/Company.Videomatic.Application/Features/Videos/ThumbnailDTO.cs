namespace Company.Videomatic.Application.Features.Videos;

public record ThumbnailDTO(
    int Id = 0,
    int VideoId = 0,
    string Location = "",
    ThumbnailResolutionDTO Resolution = ThumbnailResolutionDTO.Default,
    int Height = 0,
    int Width = 0);