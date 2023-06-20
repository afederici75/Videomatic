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
                    select video;

        var videos = await query
            .Select(p => Mapper.Map<Video, VideoDTO>(p))
            .ToListAsync();
        return new GetVideosByPlaylistResponse(Items: videos);        
    }
}
