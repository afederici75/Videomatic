using Company.Videomatic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Company.Videomatic.Application.Features.Videos;

namespace Company.Videomatic.Application.Handlers.Videos.Queries;

public class GetVideGetProviderVideoIdsQueryosHandler : IRequestHandler<GetProviderVideoIdsQuery, Result<IReadOnlyDictionary<long, string>>>
{
    public static readonly IReadOnlyDictionary<string, Expression<Func<Video, object?>>> SupportedOrderBys = new Dictionary<string, Expression<Func<Video, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(Video.Id), _ => _.Id },
        { nameof(Video.Name), _ => _.Name },
        { nameof(Video.Description), _ => _.Description },
        { "TagCount", _ => _.Tags.Count()},
        //{ "ThumbnailCount", _ => _.Thumbnails.Count()},
    };

    public GetVideGetProviderVideoIdsQueryosHandler(IDbContextFactory<VideomaticDbContext> dbFactory)
    {        
        DbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
    }

    public IDbContextFactory<VideomaticDbContext> DbFactory { get; }

    public async Task<Result<IReadOnlyDictionary<long, string>>> Handle(GetProviderVideoIdsQuery request, CancellationToken cancellationToken)
    {
        using var dbContext = DbFactory.CreateDbContext();

        var res = await dbContext.Videos.Where(v => request.VideoIds.Contains(v.Id))
                                         .Select(v => new { v.Id, v.Details.ProviderVideoId })
                                         .ToDictionaryAsync(v => v.Id.Value, v => v.ProviderVideoId);
        
        return Result.Success<IReadOnlyDictionary<long, string>>(res);
    }
}