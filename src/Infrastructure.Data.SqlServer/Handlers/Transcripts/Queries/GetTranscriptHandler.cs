using Infrastructure.Data;
using System.Linq.Expressions;

namespace Infrastructure.Data.SqlServer.Handlers.Transcripts.Queries;

public class GetTranscriptHandler : IRequestHandler<GetTranscriptsQuery, Page<TranscriptDTO>>
{    
    public GetTranscriptHandler(IDbContextFactory<VideomaticDbContext> dbContextFactory)
    {
        DbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    }

    public IDbContextFactory<VideomaticDbContext> DbContextFactory { get; }

    public async Task<Page<TranscriptDTO>> Handle(GetTranscriptsQuery request, CancellationToken cancellationToken)
    {
        using var dbContext = DbContextFactory.CreateDbContext();
        
        // Transcripts
        IQueryable<Transcript> q = dbContext.Transcripts;

        // Where
        if (request.VideoIds != null)
        {
            q = q.Where(t => request.VideoIds.Contains(t.VideoId));
        }

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            q = q.Where(t =>
                EF.Functions.FreeText(t.Language, request.SearchText));            
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
        var final = q.Select(p => new TranscriptDTO(
            p.Id,
            p.VideoId,
            p.Language,
            p.Lines.Select(x => new TranscriptLineDTO(x.Text, x.StartsAt, x.Duration)).ToArray(),
            p.Lines.Count()));

        // Returns result
        var res = await final.ToListAsync();

        return new Page<TranscriptDTO>(res, skip, take, totalCount);
    }
}