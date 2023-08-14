namespace Company.Videomatic.Application.Features.Playlists;

public record PlaylistDTO(
    int Id,
    string Name,
    ImageReference ThumbnailUrl,
    ImageReference PictureUrl,
    string? Description,
    int VideoCount);