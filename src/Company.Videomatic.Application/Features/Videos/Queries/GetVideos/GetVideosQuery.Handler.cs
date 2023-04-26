namespace Company.Videomatic.Application.Features.Videos.Queries.GetVideos;

public partial class GetVideosQuery 
{
    public class GetVideosQueryHandler : IRequestHandler<GetVideosQuery, IEnumerable<VideoDTO>>
    {
        private const int DefaultTake = 10;
    
        private readonly IVideoStorage _videoStorage;
    
        public GetVideosQueryHandler(IVideoStorage videoStorage)
        {
            _videoStorage = videoStorage ?? throw new ArgumentNullException(nameof(videoStorage));
        }

        public async Task<IEnumerable<VideoDTO>> Handle(GetVideosQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}