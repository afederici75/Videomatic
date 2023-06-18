namespace Company.Videomatic.Application.Features.Videos;

public record AddTagsToVideoCommand(int VideoId, string[] Tags) : IRequest<AddTagsToVideoResponse>;

public record AddTagsToVideoResponse(int videoId);
