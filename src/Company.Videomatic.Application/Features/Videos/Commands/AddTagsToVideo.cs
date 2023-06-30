namespace Company.Videomatic.Application.Features.Videos.Commands;

public record AddTagsToVideoCommand(long VideoId, string[] Tags) : IRequest<AddTagsToVideoResponse>;

public record AddTagsToVideoResponse(long VideoId, int Count);


