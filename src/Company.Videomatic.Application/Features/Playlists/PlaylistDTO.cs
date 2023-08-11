namespace Company.Videomatic.Application.Features.Playlists;

public record PlaylistDTO(
    int Id = 0,
    string Name = "",
    string ThumbnailUrl = "",
    string PictureUrl = "",
    string? Description = default,
    int? VideoCount = 0);