using Company.Videomatic.Application.Features.Videos;
using Company.Videomatic.Application.Features.Videos.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Queries;

public class GetVideosByIdHandler : BaseRequestHandler<GetVideosByIdQuery, GetVideosByIdResponse>
{
    public GetVideosByIdHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<GetVideosByIdResponse> Handle(GetVideosByIdQuery request, CancellationToken cancellationToken = default)
    {
        IQueryable<Video> source = DbContext.Videos.AsNoTracking();
        source = source.Where(source => request.Ids.Contains(source.Id));

        var videos = await source
            .Select(p => Mapper.Map<Video, VideoDTO>(p))
            .ToListAsync();

        return new GetVideosByIdResponse(Items: videos);
    }
}
