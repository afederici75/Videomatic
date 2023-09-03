using LinqKit;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Infrastructure.Data.SqlServer.Handlers.Videos.Queries;

public class GetVideosQueryHandler : IRequestHandler<GetVideosQuery, Page<VideoDTO>>
{    
    public GetVideosQueryHandler(IDbContextFactory<SqlServerVideomaticDbContext> factory)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }
  
    public IDbContextFactory<SqlServerVideomaticDbContext> Factory { get; }

    // GetVideosQuery
    public async Task<Page<VideoDTO>> Handle(GetVideosQuery request, CancellationToken cancellationToken = default)
    {
        using var dbContext = Factory.CreateDbContext();

        // Playlists
        IQueryable<Video> q = dbContext.Videos;

        // Where
        if (request.PlaylistIds != null)
        {
            var linkedVideoIds = from pl in dbContext.Playlists
                                 from plVid in pl.Videos
                                 where request.PlaylistIds.Contains(pl.Id)
                                 select plVid.VideoId;

            //var vidsOfPlaylists = dbContext.PlaylistVideos
            //    .Where(pv => request.PlaylistIds.Contains(pv.PlaylistId))
            //    .Select(pv => pv.VideoId);

            q = q.Where(v => linkedVideoIds.Contains(v.Id));
        }

        if (request.VideoIds != null)
        {
            q = q.Where(v => request.VideoIds.Contains(v.Id));
        }
      
        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            Expression<Func<Video, bool>> fullTextPredicate = PredicateBuilder.New<Video>(false);
            fullTextPredicate = fullTextPredicate.Or(x => EF.Functions.Contains(x.Name, $"\"{request.SearchText}\""));
            fullTextPredicate = fullTextPredicate.Or(x => EF.Functions.Contains(x.Description!, $"\"{request.SearchText}\""));
            fullTextPredicate = fullTextPredicate.Or(x => EF.Functions.Contains(x.Tags!, $"\"{request.SearchText}\""));
          
            // TODO: create a replacement of EF.Functions.FreeText so I can use this
            // in the .Data assembly and pass multiple columns. I don't have time now, but this shows how:
            // https://www.thinktecture.com/en/entity-framework-core/custom-functions-using-imethodcalltranslator-in-2-1/
            // https://www.thinktecture.com/entity-framework-core/custom-functions-using-hasdbfunction-in-2-1/
            q = q.Where(fullTextPredicate);
        }

        // OrderBy
        q = !string.IsNullOrWhiteSpace(request.OrderBy) ? q.OrderBy(request.OrderBy) : q;

        var totalCount = await q.CountAsync(cancellationToken);

        // Pagination
        var skip = request.Skip ?? 0;
        var take = request.Take ?? 10;

        q = q.Skip(skip).Take(take);

        // Projection
        //var includeThumbnail = request.SelectedThumbnail != null;
        //var preferredRes = (request.SelectedThumbnail ?? ThumbnailResolutionDTO.Default)
        //                        .ToThumbnailResolution();

        var final = q.Select(v => new VideoDTO(
            v.Id,
            $"https://www.youtube.com/videos?v=" + v.Origin.ProviderItemId,
            v.Name,
            v.Description,
            v.Tags,
            v.Thumbnail ?? v.Origin.Thumbnail,
            v.Picture ?? v.Origin.Picture,
            dbContext.Artifacts.Count(a => a.VideoId==v.Id),
            dbContext.Transcripts.Count(a => a.VideoId == v.Id),
            v.Tags.Count(),
            v.Origin.ProviderId,
            v.Origin.ProviderItemId,
            v.Origin.PublishedOn ?? DateTime.UtcNow,
            v.Origin.ChannelName,
            v.Origin.ChannelId
            ));

        // Fetches
        var res = await final.AsNoTracking().ToListAsync(cancellationToken);
      
        return new Page<VideoDTO>(res, skip, take, totalCount);
    }    
}

public static class Exts
{
    //public static IQueryable<T> XX<T, TId>(this IQueryable<T> source, Expression<Func<T, TId>> idProperty, IEnumerable<TId> ids)
    //{
    //    if ((ids == null) || !ids.Any())
    //        return source;

    //    var vidsOfPlaylists = source
    //        .Where(pv => ids.Contains(idProperty))
    //        .Select(pv => pv.VideoId);

    //    source = source.Where(v => vidsOfPlaylists.Contains(idProperty));
      

    //    return source;
    //}

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
    {
        var type = typeof(T);
        var parameter = Expression.Parameter(type, "p");
        var propertyReference = Expression.Property(parameter, propertyName);
        var lambda = Expression.Lambda(propertyReference, parameter);
        var result = Expression.Call(
                       typeof(Queryable),
                                  "OrderBy",
                                             new[] { type, propertyReference.Type },
                                                        source.Expression,
                                                                   lambda);
        return source.Provider.CreateQuery<T>(result);
    }
}