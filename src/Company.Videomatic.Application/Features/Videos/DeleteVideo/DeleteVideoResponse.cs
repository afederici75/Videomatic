using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Application.Features.Videos.DeleteVideo;

/// <summary>
/// This response is returned by DeleteVideoCommand.
/// </summary>
public record DeleteVideoResponse(Video? Video, bool Deleted);
