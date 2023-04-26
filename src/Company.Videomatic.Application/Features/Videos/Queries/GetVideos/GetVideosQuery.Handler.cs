using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Application.Features.Videos.Queries.GetVideos;

public partial class GetVideosQuery 
{
    public class GetVideosQueryHandler : IRequestHandler<GetVideosQuery, IEnumerable<VideoDTO>>
    {
        private const int DefaultTake = 10;
    
        private readonly IVideoRepository _videoStorage;
    
        public GetVideosQueryHandler(IVideoRepository videoStorage)
        {
            _videoStorage = videoStorage ?? throw new ArgumentNullException(nameof(videoStorage));
        }

        public async Task<IEnumerable<VideoDTO>> Handle(GetVideosQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Video> videos = await _videoStorage.GetVideosAsync(request.Specification);
            
            var dtos = videos.Select(v => new VideoDTO(
                Id: v.Id,
                ProviderId: v.ProviderId,
                VideoUrl: v.VideoUrl,
                Title: v.Title,
                Description: v.Description,
                Artifacts: v.Artifacts,
                Thumbnails: v.Thumbnails,
                Transcripts: v.Transcripts
                )).ToArray();

            return dtos;
        }
    }
}