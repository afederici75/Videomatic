using Company.Videomatic.Application.Features.Playlists;
using Company.Videomatic.Application.Features.Playlists.Commands;
using Company.Videomatic.Application.Features.Videos.Commands;
using MediatR;

namespace Company.Videomatic.Infrastructure.Data.Handlers;

public class VideoCommandsHandler : 
    IRequestHandler<CreateVideoCommand, CreateVideoResponse>,
    IRequestHandler<UpdateVideoCommand, UpdateVideoResponse>,
    IRequestHandler<DeleteVideoCommand, DeleteVideoResponse>,
    IRequestHandler<AddTagsToVideoCommand, AddTagsToVideoResponse>
{
    private readonly VideomaticDbContext _dbContext;
    private readonly IMapper _mapper;

    public VideoCommandsHandler(VideomaticDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<CreateVideoResponse> Handle(CreateVideoCommand request, CancellationToken cancellationToken = default)
    {
        VideoDb dbVideo = _mapper.Map<CreateVideoCommand, VideoDb>(request);

        var entry = _dbContext.Add(dbVideo);
        var res = await _dbContext.SaveChangesAsync(cancellationToken);
        
        //_dbContext.ChangeTracker.Clear();

        return new CreateVideoResponse(Id: entry.Entity.Id);
    }

    public async Task<UpdateVideoResponse> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
    {
        var newValue = _mapper.Map<UpdateVideoCommand, VideoDb>(request);

        var playlistDb = await _dbContext.Videos
            .AsTracking()
            .SingleAsync(x => x.Id == request.Id, cancellationToken);

        _mapper.Map(request, playlistDb);

        var cnt = await _dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateVideoResponse(request.Id, cnt > 0);
    }

    public async Task<DeleteVideoResponse> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
    {
        int cnt = await _dbContext
            .Videos
            .Where(x => x.Id == request.Id).
            ExecuteDeleteAsync(cancellationToken);

        return new DeleteVideoResponse(request.Id, cnt > 0);
    }

    public async Task<AddTagsToVideoResponse> Handle(AddTagsToVideoCommand request, CancellationToken cancellationToken)
    {
        var currentUserVideoTags = await _dbContext.VideoTags
            .Where(x => x.VideoId == request.VideoId)
            .ToListAsync(cancellationToken);
        
        throw new NotImplementedException();

    }
}
