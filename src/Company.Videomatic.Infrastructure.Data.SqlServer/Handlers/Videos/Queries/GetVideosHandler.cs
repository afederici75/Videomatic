﻿using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace Company.Videomatic.Infrastructure.Data.SqlServer.Handlers.Videos.Queries;

public class GetVideosHandler : IRequestHandler<GetVideosQuery, Page<VideoDTO>>
{    
    public GetVideosHandler(IDbContextFactory<VideomaticDbContext> factory)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }
    
    public IDbContextFactory<VideomaticDbContext> Factory { get; }

    // GetVideosQuery
    public async Task<Page<VideoDTO>> Handle(GetVideosQuery request, CancellationToken cancellationToken = default)
    {
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
        q = !string.IsNullOrWhiteSpace(request.OrderBy) ? q.OrderBy(request.OrderBy) : q;

        var totalCount = await q.CountAsync(cancellationToken);

        // Pagination
        var skip = request.Skip ?? 0;
        var take = request.Take ?? 10;

        q = q.Skip(skip).Take(take);

        // Projection
        var includeThumbnail = request.SelectedThumbnail != null;
        var preferredRes = (request.SelectedThumbnail ?? ThumbnailResolutionDTO.Default)
                                .ToThumbnailResolution();

        var final = q.Select(v => new VideoDTO(
            v.Id,
            v.Location,
            v.Name,
            v.Description,
            request.IncludeTags ? v.Tags.Select(t => t.Name) : null,
            v.Thumbnail,
            v.Picture,
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
        var res = await final.ToListAsync(cancellationToken);
        
        return new Page<VideoDTO>(res, skip, take, totalCount);
    }    
}
