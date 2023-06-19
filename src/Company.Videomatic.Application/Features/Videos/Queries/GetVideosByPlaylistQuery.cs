namespace Company.Videomatic.Application.Features.Videos.Queries;

public record GetVideosByPlaylistQuery(
    long PlaylistId) : IRequest<GetVideosByPlaylistResponse>;

public record GetVideosByPlaylistResponse(IEnumerable<VideoDTO> Items);

public class GetVideosByPlaylistQueryValidator : AbstractValidator<GetVideosByPlaylistQuery>
{
    public GetVideosByPlaylistQueryValidator()
    {
        RuleFor(x => x.PlaylistId).GreaterThan(0);
    }
}