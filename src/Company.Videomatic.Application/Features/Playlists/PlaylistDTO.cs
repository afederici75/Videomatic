namespace Company.Videomatic.Application.Features.Playlists;

public record PlaylistDTO(
    int Id,
    string Name,
    Thumbnail ThumbnailUrl,
    Thumbnail PictureUrl,
    string? Description,
    int VideoCount);