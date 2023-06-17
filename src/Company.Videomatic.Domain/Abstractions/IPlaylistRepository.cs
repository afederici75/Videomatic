using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Domain.Abstractions;

public record GetPlaylistById(long Id, string[]? Includes = default);

public interface IPlaylistRepository
{
    public Task<Playlist?> GetByIdAsync(GetPlaylistById args, CancellationToken cancellationToken = default);
    public Task<Playlist> CreateAsync(Playlist playlist, CancellationToken cancellationToken = default);
    public Task<Playlist> UpdateAsync(Playlist playlist, CancellationToken cancellationToken = default);
}

public interface IVideoRepository
{ 

}