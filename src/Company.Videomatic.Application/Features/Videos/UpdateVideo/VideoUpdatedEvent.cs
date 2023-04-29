namespace Company.Videomatic.Application.Features.Videos.UpdateVideo;

/// <summary>
/// This event is published when a video is updated.
/// </summary>
/// <param name="VideoId"></param>
public record VideoUpdatedEvent(int VideoId);