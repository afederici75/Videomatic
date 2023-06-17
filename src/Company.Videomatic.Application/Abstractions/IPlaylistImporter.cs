namespace Company.Videomatic.Application.Abstractions;

/// <summary>
/// A service that imports playlist information.
/// </summary>
public interface IPlaylistImporter
{
    public Task<VideoCollection> ImportAsync(Uri location);
}