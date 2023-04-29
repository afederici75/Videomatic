using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Application.Abstractions;

/// <summary>
/// A service that analyzes videos and produces artifacts.
/// </summary>
public interface IVideoAnalyzer
{
    /// <summary>
    /// Summarizes the video and returns an artifact.
    /// </summary>
    Task<Artifact> SummarizeVideoAsync(Video video);

    /// <summary>
    /// Reviews the video and returns an artifact.
    /// </summary>
    Task<Artifact> ReviewVideoAsync(Video video);
}
