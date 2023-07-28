using Company.SharedKernel.Abstractions;
using Company.Videomatic.Application.Features.Videos;
using Company.Videomatic.Application.Features.Videos.Queries;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace Company.Videomatic.Infrastructure.Data.SqlServer.Handlers.Videos.Queries;

public class GetVideosHandler : IRequestHandler<GetVideosQuery, Page<VideoDTO>>
{
    public static readonly IReadOnlyDictionary<string, Expression<Func<Video, object?>>> SupportedOrderBys = new Dictionary<string, Expression<Func<Video, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(VideoDTO.Id), _ => _.Id },
        { nameof(VideoDTO.Name), _ => _.Name },
        { nameof(VideoDTO.Description), _ => _.Description }
    };

    public GetVideosHandler(IDbContextFactory<VideomaticDbContext> factory)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }
    
    public IDbContextFactory<VideomaticDbContext> Factory { get; }

    // GetVideosQuery
    public async Task<Page<VideoDTO>> Handle(GetVideosQuery request, CancellationToken cancellationToken = default)
    {
        var skip = request.Skip ?? 0;
        var take = request.Take ?? 10;

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

        q = !request.VideoIds.IsNullOrEmpty() ? q.Where(v => request.VideoIds!.Contains(v.Id)) : q;
        
        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            // TODO: create a replacement of EF.Functions.FreeText so I can use this
            // in the .Data assembly and pass multiple columns. I don't have time now, but this shows how:
            // https://www.thinktecture.com/en/entity-framework-core/custom-functions-using-imethodcalltranslator-in-2-1/
            // https://www.thinktecture.com/entity-framework-core/custom-functions-using-hasdbfunction-in-2-1/
            q = q.Where(v => 
                EF.Functions.FreeText(v.Name, request.SearchText) ||
                ((v.Description != null) && EF.Functions.FreeText(v.Description, request.SearchText))
            );
        }

        // OrderBy
        q = !string.IsNullOrWhiteSpace(request.OrderBy) ? q.OrderBy(request.OrderBy, SupportedOrderBys) : q;
        
        var totalCount = await q.CountAsync();

        // Pagination
        q = q.Skip(skip).Take(take);

        // ---

        var includeThumbnail = request.SelectedThumbnail != null;
        var preferredRes = (request.SelectedThumbnail ?? ThumbnailResolutionDTO.Default)
                                .ToThumbnailResolution();

        // Projection
        var final = q.Select(v => new VideoDTO(
            v.Id,
            v.Location,
            v.Name,
            v.Description,
            request.IncludeTags ? v.Tags.Select(t => t.Name) : null,
            includeThumbnail ? v.Thumbnails.Single(t => t.Resolution == preferredRes).Location : null,
            dbContext.Artifacts.Count(a => a.VideoId==v.Id),
            dbContext.Transcripts.Count(a => a.VideoId == v.Id),
            v.Tags.Count(),
            v.Details.Provider,
            v.Details.ProviderVideoId,
            v.Details.VideoPublishedAt,
            v.Details.VideoOwnerChannelTitle,
            v.Details.VideoOwnerChannelId
            ));

        // Counts
        var res = await final.ToListAsync();
        
        return new Page<VideoDTO>(res, skip, take, totalCount);
    }    
}
