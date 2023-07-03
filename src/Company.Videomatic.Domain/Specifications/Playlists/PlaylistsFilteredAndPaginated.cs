using Company.Videomatic.Domain.Extensions;
using System.Linq.Expressions;

namespace Company.Videomatic.Domain.Specifications.Playlists;

public class PlaylistsFilteredAndPaginated : Specification<Playlist>, IPaginatedSpecification<Playlist>
{
    public static readonly IReadOnlyDictionary<string, Expression<Func<Playlist, object?>>> SupportedOrderBys = new Dictionary<string, Expression<Func<Playlist, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(Playlist.Id), _ => _.Id },
        { nameof(Playlist.Name), _ => _.Name },
        { nameof(Playlist.Description), _ => _.Description }
    };

    public PlaylistsFilteredAndPaginated(string? searchText = default,
                                         int? page = default,
                                         int? pageSize = default,
                                                                                                                               string? orderBy = default)
    {
        // searchText is included in Name and Description
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            Query.Where(p => p.Name.Contains(searchText) ||
                             p.Description!.Contains(searchText));
        }

        // OrderBy
        Query.OrderByExpressions(orderBy, SupportedOrderBys);
    }

    public int Page { get; }
    public int PageSize { get; }
}
