using Company.Videomatic.Infrastructure.Data;
using System.Linq.Expressions;

namespace Company.Videomatic.Application.Handlers.Transcripts.Queries;

public class GetTranscriptHandler : IRequestHandler<GetTranscriptsQuery, PageResult<TranscriptDTO>>
{
    public static readonly IReadOnlyDictionary<string, Expression<Func<Transcript, object?>>> SupportedOrderBys = new Dictionary<string, Expression<Func<Transcript, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(Transcript.Id), _ => _.Id },
        { nameof(Transcript.Language), _ => _.Language },
        { "LineCount", _ => _.Lines.Count },
    };


    public GetTranscriptHandler(VideomaticDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    readonly VideomaticDbContext _dbContext;

    public async Task<PageResult<TranscriptDTO>> Handle(GetTranscriptsQuery request, CancellationToken cancellationToken)
    {
        var pageIdx = request.Page ?? 1;
        var pageSize = request.PageSize ?? 10;

        // Transcripts
        IQueryable<Transcript> q = _dbContext.Transcripts;

        // Where
        if (request.VideoIds != null)
        {
            q = q.Where(t => request.VideoIds.Contains(t.VideoId));
        }

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            q = q.Where(p => p.Language.Contains(request.SearchText));
        }

        // OrderBy
        if (!string.IsNullOrWhiteSpace(request.OrderBy))
        {
            q = q.OrderBy(request.OrderBy, SupportedOrderBys);
        }

        // Projection
        var final = q.Select(p => new TranscriptDTO(
            p.Id,
            p.VideoId,
            p.Language,
            p.Lines.Count()));

        // Counts
        var totalCount = await final.CountAsync();
        var res = await final.ToListAsync();

        return new PageResult<TranscriptDTO>(res, pageIdx, pageSize, totalCount);
    }
}