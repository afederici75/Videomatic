using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Application.Features.Videos.UpdateVideo;

public record UpdateVideoResponse(Video? Video, bool Updated);