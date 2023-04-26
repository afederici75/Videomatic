using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Application.Abstractions;

public interface IVideoAnalyzer
{
    Task<Artifact> SummarizeVideoAsync(Video video);
    Task<Artifact> ReviewVideoAsync(Video video);
}
