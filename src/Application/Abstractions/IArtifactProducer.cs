namespace Application.Abstractions;

public interface IArtifactProducer
{
    Task CreateEmbeddings(
        string provider,
        string uniqueId,
        string fullTranscriptText,
        CancellationToken cancellationToken = default);

    Task CreateEncodings(
        VideoId videoId,
        CancellationToken cancellationToken = default);    
}