namespace Company.Videomatic.Application.Features.Playlists;

public record GetPlaylistByIdQuery(
    long Id,
    string[]? Includes = default) : IRequest<Playlist?>;
