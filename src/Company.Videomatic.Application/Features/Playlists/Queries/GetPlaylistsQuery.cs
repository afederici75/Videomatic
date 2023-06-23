namespace Company.Videomatic.Application.Features.Playlists.Queries;

public record GetPlaylistsQuery(
    string? Filter = null,
    string? OrderBy = null,
    int? Page = null,
    int? PageSize = null,
    bool IncludeCounts = false) : IRequest<PageResult<PlaylistDTO>>
{    
}

internal class GetPlaylistsQueryValidator : AbstractValidator<GetPlaylistsQuery>
{
    public GetPlaylistsQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }
}
