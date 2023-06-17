using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Domain.Abstractions;

public record PlaylistByIdQuery(long Id, string[]? Includes = default);

public record CreatePlaylistCommand(string Name, string? Description);

public record UpdatePlaylistCommand(string? Name, string? Description);

public interface IPlaylistRepository
{
    public Task<Playlist?> GetByIdAsync(PlaylistByIdQuery query, CancellationToken cancellationToken = default);
    public Task<Playlist> CreateAsync(CreatePlaylistCommand command, CancellationToken cancellationToken = default);
    public Task<Playlist> UpdateAsync(Playlist playlist, CancellationToken cancellationToken = default);
}

public interface IVideoRepository
{ 

}