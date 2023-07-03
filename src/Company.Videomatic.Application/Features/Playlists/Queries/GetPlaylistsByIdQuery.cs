using Company.Videomatic.Domain.Abstractions;

namespace Company.Videomatic.Application.Features.Playlists.Queries;

public record GetPlaylistsByIdQuery(
    long[] PlaylistIds,
    string? SearchText = null,
    string? OrderBy = null,
    int? Page = null,
    int? PageSize = null) : IRequest<IEnumerable<PlaylistDTO>>;

internal class GetPlaylistsByIdQueryValidator : AbstractValidator<GetPlaylistsByIdQuery>
{
    public GetPlaylistsByIdQueryValidator()
    {
        RuleFor(x => x.PlaylistIds).NotEmpty();
        
        When(x => x.SearchText is not null, () =>
        {
            RuleFor(x => x.SearchText).NotEmpty();
        });

        When(x => x.OrderBy is not null, () =>
        {
            RuleFor(x => x.OrderBy).NotEmpty();
        });

        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }
}
