using Company.Videomatic.Domain.Extensions;
using System.Linq.Expressions;

namespace Company.Videomatic.Domain.Specifications.Artifacts;

public class ArtifactsFilteredAndPaginated : Specification<Artifact>//, IPaginatedSpecification<Artifact>
{
    public static readonly IReadOnlyDictionary<string, Expression<Func<Artifact, object?>>> SupportedOrderBys = new Dictionary<string, Expression<Func<Artifact, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(Artifact.Id), _ => _.Id },
        { nameof(Artifact.Name), _ => _.Name },
        { nameof(Artifact.Type), _ => _.Type }
    };

    public ArtifactsFilteredAndPaginated(string? searchText = default,
                                         int? page = default,
                                         int? pageSize = default,
                                         string? orderBy = default)
    {
        // searchText is included in Name and Description
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            Query.Where(p => p.Name.Contains(searchText) ||                             
                             p.Type!.Contains(searchText) ||
                             p.Text!.Contains(searchText));
        }

        // OrderBy
        Query.OrderByText(orderBy, SupportedOrderBys);
    }

    public int Page { get; }
    public int PageSize { get; }
}
