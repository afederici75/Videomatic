using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Application.Features.Videos.Commands.DeleteVideo;

/// <summary>
/// This response is returned by DeleteVideoCommand.
/// </summary>
public record DeleteVideoResponse(Video? Video, bool Deleted);
