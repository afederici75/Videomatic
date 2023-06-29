namespace Company.Videomatic.Application.Features.Videos.Commands;

public record AssignVideoThumnbailsCommand(
    long VideoId,
    IEnumerable<ThumbnailInfo> Thumbnails) : IRequest<AssignVideoThumnbailsResponse>;

// I could make ThumbnailInfo a sub-record merging it in AssignVideoThumnbailsCommand!
public record ThumbnailInfo(
    string Location,
    ThumbnailResolutionDTO Resolution,
    int Height,
    int Width);

public record AssignVideoThumnbailsResponse(
    long VideoId,
    ThumbnailResolutionDTO[] Thumbnails);

