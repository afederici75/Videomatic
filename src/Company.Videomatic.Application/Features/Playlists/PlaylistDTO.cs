namespace Company.Videomatic.Application.Features.Playlists;

public record PlaylistDTO(
    long Id = 0,
    string Name = "",
    string? Description = default,

    long? VideoCount = 0);