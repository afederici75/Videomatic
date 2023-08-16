namespace Application.Features.Videos;

public readonly record struct ThumbnailDTO(
    int Id = 0,
    int VideoId = 0,
    string Location = "",
    int Height = 0,
    int Width = 0);