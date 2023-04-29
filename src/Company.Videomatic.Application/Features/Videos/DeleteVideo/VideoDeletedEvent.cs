using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Application.Features.Videos.DeleteVideo;

/// <summary>
/// This event is published when a video is deleted.
/// </summary>
public record VideoDeletedEvent(Video Video) : INotification;
