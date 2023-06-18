using Company.Videomatic.Application.Features.Playlists;
using Company.Videomatic.Application.Features.Videos;
using MediatR;

namespace Company.Videomatic.Infrastructure.Data.Handlers;

public class VideoCommandsHandler : 
    IRequestHandler<CreateVideoCommand, Video>
{
    private readonly VideomaticDbContext _dbContext;
    private readonly IMapper _mapper;

    public VideoCommandsHandler(VideomaticDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Video> Handle(CreateVideoCommand request, CancellationToken cancellationToken = default)
    {
        VideoDb dbVideo = _mapper.Map<CreateVideoCommand, VideoDb>(request);

        var entry = _dbContext.Add(dbVideo);
        var res = await _dbContext.SaveChangesAsync(cancellationToken);

        //_dbContext.ChangeTracker.Clear();

        return _mapper.Map<VideoDb, Video>(entry.Entity);
    }
}
