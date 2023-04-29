using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Application.Abstractions;

/// <summary>
/// A service that imports video information.
/// </summary>
public interface IVideoImporter
{
    /// <summary>
    /// Imports the information of a video from a location.
    /// </summary>
    /// <param name="location">The location of the video.</param>
    /// <returns>A new instance of <see cref="Video"/>. </returns>
    public Task<Video> ImportAsync(Uri location);
}
