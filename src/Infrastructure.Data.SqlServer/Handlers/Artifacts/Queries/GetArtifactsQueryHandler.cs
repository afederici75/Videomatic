using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Data.SqlServer.Handlers.Artifacts.Queries;

public class GetArtifactHandler : IRequestHandler<GetArtifactsQuery, Page<ArtifactDTO>>
{    
    public GetArtifactHandler(IDbContextFactory<SqlServerVideomaticDbContext> dbContextFactory)
    {
        DbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    }

    public IDbContextFactory<SqlServerVideomaticDbContext> DbContextFactory { get; }

    public async Task<Page<ArtifactDTO>> Handle(GetArtifactsQuery request, CancellationToken cancellationToken)
    {
        using var dbContext = DbContextFactory.CreateDbContext();

        // Artifacts
        IQueryable<Artifact> q = dbContext.Artifacts;

        // Where
        if (request.ArtifactIds != null)
        {
            // This line contained a bug before I made the ids strongly typed in requests, too.
            // Instead of passing a.Id I was passing a.VideoId and it did not show a problem as integer!!!
            q = q.Where(a => request.ArtifactIds.Contains(a.Id));             
        }

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            q = q.Where(a =>
                EF.Functions.FreeText(a.Name, request.SearchText) ||
                ((a.Text != null) && EF.Functions.FreeText(a.Text, request.SearchText))
            );
        }

        // OrderBy
        if (!string.IsNullOrWhiteSpace(request.OrderBy))
        {
            q = q.OrderBy(request.OrderBy);
        }

        var totalCount = await q.CountAsync();

        // Pagination
        var skip = request.Skip ?? 0;
        var take = request.Take ?? 10;

        q = q.Skip(skip).Take(take);
        
        // Projection
        var final = q.Select(a => new ArtifactDTO(
            a.Id,
            a.VideoId,
            a.Name,
            a.Type,
            a.Text));

        // Result
        var res = await final.ToListAsync();

        return new Page<ArtifactDTO>(res, skip, take, totalCount);
    }
}