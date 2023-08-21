using Domain.Videos;

namespace Application.Abstractions;

public interface IArtifactProducer
{
    Task GenerateSummary(VideoId videoId);
    Task GenerateTLDR(VideoId videoId);
    Task GenerateListOfTopics(VideoId videoId);
    Task Translate(VideoId videoId, string language);
}