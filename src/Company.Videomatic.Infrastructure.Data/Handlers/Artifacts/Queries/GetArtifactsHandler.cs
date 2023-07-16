using Company.Videomatic.Infrastructure.Data;
using System.Linq.Expressions;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Artifacts.Queries;

public class GetArtifactHandler : IRequestHandler<GetArtifactsQuery, Page<ArtifactDTO>>
{
    public static readonly IReadOnlyDictionary<string, Expression<Func<Artifact, object?>>> SupportedOrderBys = new Dictionary<string, Expression<Func<Artifact, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(Artifact.Id), _ => _.Id },
        { nameof(Artifact.Name), _ => _.Type },
        { nameof(Artifact.Type), _ => _.Type },
        { nameof(Artifact.Text), _ => _.Text },

    };


    public GetArtifactHandler(VideomaticDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    readonly VideomaticDbContext _dbContext;

    public async Task<Page<ArtifactDTO>> Handle(GetArtifactsQuery request, CancellationToken cancellationToken)
    {
        var pageIdx = request.Page ?? 1;
        var pageSize = request.PageSize ?? 10;

        // Artifacts
        IQueryable<Artifact> q = _dbContext.Artifacts;

        // Where
        if (request.ArtifactIds != null)
        {
            q = q.Where(a => request.ArtifactIds.Contains(a.VideoId));
        }

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            q = q.Where(a => a.Name.Contains(request.SearchText) ||
                             a.Type.Contains(request.SearchText) || 
                             ((a.Text != null) && a.Text.Contains(request.SearchText)));
        }

        // OrderBy
        if (!string.IsNullOrWhiteSpace(request.OrderBy))
        {
            q = q.OrderBy(request.OrderBy, SupportedOrderBys);
        }

        // Projection
        var final = q.Select(a => new ArtifactDTO(
            a.Id,
            a.VideoId,
            a.Name,
            a.Type,
            a.Text));

        // Counts
        var totalCount = await final.CountAsync();
        var res = await final.ToListAsync();

        return new Page<ArtifactDTO>(res, pageIdx, pageSize, totalCount);
    }
}