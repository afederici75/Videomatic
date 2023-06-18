namespace Company.Videomatic.Application.Features.Playlists;

public record GetPlaylistsQuery(
    long[]? Ids = default, 
    string[]? Includes = default,
    int? Take = default, 
    int? Skip = default) : IRequest<GetPlaylistsResponse>;

public record GetPlaylistsResponse(IEnumerable<PlaylistDTO> playlists);