namespace Company.Videomatic.Application.Features.Videos.Queries.GetVideos;

public partial class GetVideosQuery : IRequest<GetVideosResponse>
{
    public class GetVideosQueryHandler : IRequestHandler<GetVideosQuery, GetVideosResponse>
    {
        private const int DefaultTake = 10;

        private readonly IVideoStorage _videoStorage;

        public GetVideosQueryHandler(IVideoStorage videoStorage)
        {
            _videoStorage = videoStorage ?? throw new ArgumentNullException(nameof(videoStorage));
        }

        public async Task<GetVideosResponse> Handle(GetVideosQuery request, CancellationToken cancellationToken)
        {
            Video[] videos = await _videoStorage.GetVideosAsync(
                filter: q => ApplyFilter(q, request.Filter),
                sort: q => ApplySort(q, request.Sort),
                paging: q => ApplyPaging(q, request.Paging), 
                cancellationToken: cancellationToken);

            var dtos = videos.Select(v => new GetVideosDTO(
                 Id: v.Id,
                 ProviderId: v.ProviderId,
                 VideoUrl: v.VideoUrl,
                 Title: v.Title,
                 Description: v.Description,
                 Artifacts: v.Artifacts,
                 Thumbnails: v.Thumbnails,
                 Transcripts: v.Transcripts))
                .ToArray();

            var response = new GetVideosResponse(dtos);

            return response;
        }

        private IQueryable<Video> ApplyPaging(IQueryable<Video> query, PagingOptions? pagingOptions)
        {
            return query.Skip(pagingOptions?.Skip ?? 0)
                        .Take(pagingOptions?.Take ?? DefaultTake);
        }

        private IQueryable<Video> ApplySort(IQueryable<Video> query, SortOptions? sortOptions)
        {
            throw new NotImplementedException();
        }

        private IQueryable<Video> ApplyFilter(IQueryable<Video> query, VideosFilter? filter)
        {
            if (filter is null)
                return query;

            if (filter.Ids is not null)
            {
                query = query.Where(v => filter.Ids.Contains(v.Id));
            }

            if (filter.Title is not null)
            { 
                query = query.Where(v => v.Title!.Contains(filter.Title));
            }

            if (filter.Description is not null)
            {
                query = query.Where(v => v.Description!.Contains(filter.Description));
            }

            return query;
        }
    }
}