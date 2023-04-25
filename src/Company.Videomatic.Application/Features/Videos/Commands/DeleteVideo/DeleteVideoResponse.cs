namespace Company.Videomatic.Application.Features.Videos.Commands.DeleteVideo;

/// <summary>
/// This response is returned by DeleteVideoCommand.
/// </summary>
public record DeleteVideoResponse(int VideoId, bool Deleted);
