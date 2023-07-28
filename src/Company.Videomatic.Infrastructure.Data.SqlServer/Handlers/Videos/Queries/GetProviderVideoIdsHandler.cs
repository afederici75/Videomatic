using Company.Videomatic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Company.Videomatic.Application.Features.Videos;

namespace Company.Videomatic.Infrastructure.Data.SqlServer.Handlers.Videos.Queries;

public class GetVideGetProviderVideoIdsQueryosHandler : IRequestHandler<GetVideoIdsOfProviderQuery, Result<IEnumerable<VideoIdAndProviderVideoIdDTO>>>
{
    public GetVideGetProviderVideoIdsQueryosHandler(IDbContextFactory<VideomaticDbContext> dbFactory)
    {        
        DbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
    }

    public IDbContextFactory<VideomaticDbContext> DbFactory { get; }

    public async Task<Result<IEnumerable<VideoIdAndProviderVideoIdDTO>>> Handle(GetVideoIdsOfProviderQuery request, CancellationToken cancellationToken)
    {
        using var dbContext = DbFactory.CreateDbContext();

        var res = await dbContext.Videos
            .Where(v => request.VideoIds.Contains(v.Id))
            .Select(v => new VideoIdAndProviderVideoIdDTO(v.Id, v.Details.ProviderVideoId))
            .ToListAsync();
        
        return Result.Success<Result<IEnumerable<VideoIdAndProviderVideoIdDTO>>>(res);
    }
}