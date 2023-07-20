using Company.Videomatic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Company.Videomatic.Application.Features.Videos;
using Microsoft.EntityFrameworkCore.Internal;

namespace Company.Videomatic.Application.Handlers.Videos.Queries;

public class GetVideosHandler : IRequestHandler<GetVideosQuery, Page<VideoDTO>>
{
    public static readonly IReadOnlyDictionary<string, Expression<Func<Video, object?>>> SupportedOrderBys = new Dictionary<string, Expression<Func<Video, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(Video.Id), _ => _.Id },
        { nameof(Video.Name), _ => _.Name },
        { nameof(Video.Description), _ => _.Description },
        { "TagCount", _ => _.Tags.Count()},        
    };

    public GetVideosHandler(IDbContextFactory<VideomaticDbContext> factory)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }
    
    public IDbContextFactory<VideomaticDbContext> Factory { get; }

    // GetVideosQuery
    public async Task<Page<VideoDTO>> Handle(GetVideosQuery request, CancellationToken cancellationToken = default)
    {
        var pageIdx = request.Page ?? 1;
        var pageSize = request.PageSize ?? 10;

        using var dbContext = Factory.CreateDbContext();

        // Playlists
        IQueryable<Video> q = dbContext.Videos;

        // Where
        if (request.PlaylistIds != null)
        {
            var vidsOfPlaylists = dbContext.PlaylistVideos
                .Where(pv => request.PlaylistIds.Contains(pv.PlaylistId))
                .Select(pv => pv.VideoId);

            q = q.Where(v => vidsOfPlaylists.Contains(v.Id));
        }

        if (request.VideoIds != null)
        {
            q = q.Where(v => request.VideoIds.Contains(v.Id));
        }

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            q = q.Where(v => v.Name.Contains(request.SearchText) ||                              
                             ((v.Description != null) && v.Description.Contains(request.SearchText)));
        }

        // OrderBy
        if (!string.IsNullOrWhiteSpace(request.OrderBy))
        {
            q = q.OrderBy(request.OrderBy, SupportedOrderBys);
        }

        var includeThumbnail = request.IncludeThumbnail != null;
        var preferredRes = (request.IncludeThumbnail ?? ThumbnailResolutionDTO.Default)
            .ToThumbnailResolution();

        // Projection
        var final = q.Select(v => new VideoDTO(
            v.Id,
            v.Location,
            v.Name,
            v.Description,
            request.IncludeTags ? v.Tags.Select(t => t.Name) : null,
            includeThumbnail ? v.Thumbnails.Single(t => t.Resolution==preferredRes).Location : null,
            0, // art count
            0, // TranscriptCount
            v.Details.Provider,
            v.Details.ProviderVideoId,
            v.Details.VideoPublishedAt,
            v.Details.VideoOwnerChannelTitle,
            v.Details.VideoOwnerChannelId
            ));

        // Counts
        var res = await final.ToListAsync();
        var totalCount = await final.CountAsync();
        
        return new Page<VideoDTO>(res, pageIdx, pageSize, totalCount);
    }
}