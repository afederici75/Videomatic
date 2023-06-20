using Company.Videomatic.Application.Features.Model;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Queries;

public class GetVideosByPlaylistHandler : BaseRequestHandler<GetVideosByPlaylistQuery, GetVideosByPlaylistResponse>
{
    public GetVideosByPlaylistHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<GetVideosByPlaylistResponse> Handle(GetVideosByPlaylistQuery request, CancellationToken cancellationToken = default)
    {
        var query = from video in DbContext.Videos.AsNoTracking()
                    join playlistVideo in DbContext.PlaylistVideos.AsNoTracking()
                    on video.Id equals playlistVideo.VideoId
                    where playlistVideo.PlaylistId == request.PlaylistId
                    select new 
                    {
                        Id = video.Id, 
                        Title = video.Title, 
                        Description = video.Description, 
                        Location = video.Location,
                        PlaylistCount = (int?)(request.IncludeCounts ? video.Playlists.Count : null),
                        ArtifactCount = (int?)(request.IncludeCounts ? video.Artifacts.Count : null),
                        ThumbnailCount = (int?)(request.IncludeCounts ? video.Thumbnails.Count : null),
                        TranscriptCount = (int?)(request.IncludeCounts ? video.Transcripts.Count : null),
                        TagCount = (int?)(request.IncludeCounts ? video.VideoTags.Count : null),
                        Thumbnail = (request.IncludeThumbnail != null) ? video.Thumbnails.FirstOrDefault(t => t.Resolution==request.IncludeThumbnail) : null
                    };

        var videos = await query
            .Select(v => new VideoDTO(
                v.Id,
                v.Location,
                v.Title,
                v.Description,
                v.PlaylistCount,
                v.ArtifactCount,
                v.ThumbnailCount,
                v.TranscriptCount,
                v.TagCount,
                Mapper.Map<Thumbnail, ThumbnailDTO>(v.Thumbnail)
                ))
            .ToListAsync();

        return new GetVideosByPlaylistResponse(Items: videos);        
    }
}