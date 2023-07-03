namespace Company.Videomatic.Application.Features.Videos.Commands;

public record SetVideoTags(long VideoId, string[] Tags) : IRequest<SetVideoTagsResponse>;

public record SetVideoTagsResponse(long VideoId, int Count);