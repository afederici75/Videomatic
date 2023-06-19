namespace Company.Videomatic.Application.Features.Videos.Commands;

public record AddTagsToVideoCommand(int VideoId, string[] Tags) : IRequest<AddTagsToVideoResponse>;

public record AddTagsToVideoResponse(int videoId);
