using System.Linq.Expressions;

namespace Company.Videomatic.Infrastructure.Data.SqlServer.Handlers.Artifacts.Queries;

public class GetArtifactHandler : IRequestHandler<GetArtifactsQuery, Page<ArtifactDTO>>
{
    public static readonly IReadOnlyDictionary<string, Expression<Func<Artifact, object?>>> SupportedOrderBys = new Dictionary<string, Expression<Func<Artifact, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(Artifact.Id), _ => _.Id },
        { nameof(Artifact.Name), _ => _.Type },
        { nameof(Artifact.Type), _ => _.Type },
        { nameof(Artifact.Text), _ => _.Text },
    };


    public GetArtifactHandler(IDbContextFactory<VideomaticDbContext> dbContextFactory)
    {
        DbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    }

    public IDbContextFactory<VideomaticDbContext> DbContextFactory { get; }

    public async Task<Page<ArtifactDTO>> Handle(GetArtifactsQuery request, CancellationToken cancellationToken)
    {
        using var dbContext = DbContextFactory.CreateDbContext();

        var skip = request.Skip ?? 0;
        var take = request.Take ?? 10;

        // Artifacts
        IQueryable<Artifact> q = dbContext.Artifacts;

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
            q = q.OrderBy(request.OrderBy);
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

        return new Page<ArtifactDTO>(res, skip, take, totalCount);
    }
}