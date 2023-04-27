using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Application.Abstractions;

public class MockVideoAnalyzer : IVideoAnalyzer
{

    public Task<Artifact> ReviewVideoAsync(Video video)
    {
        return Task.FromResult(new Artifact("Mock Review", "Some text would be here"));
    }

    public Task<Artifact> SummarizeVideoAsync(Video video)
    {
        return Task.FromResult(new Artifact("Mock Summary", "Some text would be here"));
    }
}