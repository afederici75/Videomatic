using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Application.Features.Videos.Commands.UpdateVideo;

public record UpdateVideoResponse(Video? Video, bool Updated);