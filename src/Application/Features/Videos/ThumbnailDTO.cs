namespace Application.Features.Videos;

public record ThumbnailDTO(
    int Id = 0,
    int VideoId = 0,
    string Location = "",
    int Height = 0,
    int Width = 0);