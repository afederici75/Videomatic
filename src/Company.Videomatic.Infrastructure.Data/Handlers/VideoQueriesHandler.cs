using Company.Videomatic.Application.Features.Playlists.Queries;
using Company.Videomatic.Application.Features.Videos.Queries;
using MediatR;
using Company.Videomatic.Application.Features.Videos;

namespace Company.Videomatic.Infrastructure.Data.Handlers;

public class VideoQueriesHandler :
    IRequestHandler<GetVideosByIdQuery, GetVideosByIdResponse>
{
    private readonly VideomaticDbContext _dbContext;
    private readonly IMapper _mapper;

    public VideoQueriesHandler(VideomaticDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetVideosByIdResponse> Handle(GetVideosByIdQuery request, CancellationToken cancellationToken = default)
    {
        IQueryable<VideoDb> source = _dbContext.Videos.AsNoTracking();
        source = source.Where(source => request.Ids.Contains(source.Id));

        var videos = await source
            .Select(p => _mapper.Map<VideoDb, VideoDTO>(p))
            .ToListAsync();

        return new GetVideosByIdResponse(Items: videos);
    }
}
