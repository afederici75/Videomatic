namespace Company.Videomatic.Application.Features.Videos.Commands;

public record AddPlaylistToVideo(int VideoId, int[] CollectionIds) : IRequest<AddCollectionsToVideoResponse>;

public record AddCollectionsToVideoResponse(int addedToCount);