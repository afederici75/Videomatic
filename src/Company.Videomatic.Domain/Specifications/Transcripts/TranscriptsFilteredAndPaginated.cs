using Company.Videomatic.Domain.Extensions;
using System.Linq.Expressions;

namespace Company.Videomatic.Domain.Specifications.Transcripts;

public class TranscriptsFilteredAndPaginated : Specification<Transcript>//, IPaginatedSpecification<Transcript>
{
    public static readonly IReadOnlyDictionary<string, Expression<Func<Transcript, object?>>> SupportedOrderBys = new Dictionary<string, Expression<Func<Transcript, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(Transcript.Id), _ => _.Id },
        { nameof(Transcript.Language), _ => _.Language },
        //{ nameof(Transcript.Description), _ => _.Description }
    };

    public TranscriptsFilteredAndPaginated(string? searchText = default,
                                         int? page = default,
                                         int? pageSize = default,
                                                                                                                               string? orderBy = default)
    {
        // searchText is included in Name and Description
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            Query.Where(t => t.Lines.Any(lne => lne.Text.Contains(searchText)) ||
                             t.Language.Contains(searchText));
        }

        // OrderBy
        Query.OrderByText(orderBy, SupportedOrderBys);
    }

    public int Page { get; }
    public int PageSize { get; }
}