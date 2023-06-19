namespace Company.Videomatic.Application.Features.Playlists.Queries;

public record GetPlaylistsQuery(
    string? Filter = null,
    string[]? OrderBy = null,
    int? Take = default,
    int? Skip = default) : IRequest<GetPlaylistsResponse>;

public record GetPlaylistsResponse(IEnumerable<PlaylistDTO> playlists);

public class GetPlaylistsQueryValidator : AbstractValidator<GetPlaylistsQuery>
{
    public GetPlaylistsQueryValidator()
    {
        RuleFor(x => x.Take).InclusiveBetween(1, 50);
        RuleFor(x => x.Take).GreaterThanOrEqualTo(0);
    }
}