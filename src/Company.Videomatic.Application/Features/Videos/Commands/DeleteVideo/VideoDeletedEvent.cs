namespace Company.Videomatic.Application.Features.Videos.Commands.DeleteVideo;

/// <summary>
/// This event is published when a video is deleted.
/// </summary>
public record VideoDeletedEvent(int VideoId) : INotification;