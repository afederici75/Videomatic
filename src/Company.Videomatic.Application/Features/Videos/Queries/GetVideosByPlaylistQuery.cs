namespace Company.Videomatic.Application.Features.Videos.Queries;

public record PlaylistFilter(
    long PlaylistId= 0    
    );

public record GetVideosByPlaylistQuery(
    long PlaylistId,
    bool IncludeCounts = false,
    ThumbnailResolution? IncludeThumbnail = null) : IRequest<GetVideosByPlaylistResponse>;

public record GetVideosByPlaylistResponse(IEnumerable<VideoDTO> Items);

public class GetVideosByPlaylistQueryValidator : AbstractValidator<GetVideosByPlaylistQuery>
{
    public GetVideosByPlaylistQueryValidator()
    {
        RuleFor(x => x.PlaylistId).GreaterThan(0);
    }
}