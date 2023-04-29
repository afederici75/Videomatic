namespace Company.Videomatic.Application.Features.Videos.Commands.DeleteVideo;

public record VideoImportedEvent(int VideoId, int ThumbNailCount, int TranscriptCount, int ArtifactsCount) : INotification;