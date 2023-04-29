namespace Company.Videomatic.Application.Features.Videos.ImportVideo;

public record VideoImportedEvent(int VideoId, int ThumbNailCount, int TranscriptCount, int ArtifactsCount) : INotification;