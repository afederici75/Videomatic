using Company.Videomatic.Application.Features.Model;

namespace Company.Videomatic.Application.Features.Videos.Commands;

public record AddThumnbailsToVideoCommand(
    long VideoId,
    IEnumerable<ThumbnailPayload> Thumbnails) : IRequest<AddThumbnailsToVideoResponse>;

public record ThumbnailPayload(
    string Location,
    ThumbnailResolution Resolution,
    int Height,
    int Width);

public record AddThumbnailsToVideoResponse(
    long VideoId, 
    IReadOnlyDictionary<ThumbnailResolution, long> Ids);

