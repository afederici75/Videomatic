using Company.Videomatic.Application.Features.Playlists.Commands;
using MediatR;
using System.Security.Cryptography;

namespace Company.Videomatic.Infrastructure.Data.Handlers;

public class PlaylistCommandsHandler :
    IRequestHandler<CreatePlaylistCommand, CreatePlaylistResponse>,
    IRequestHandler<DeletePlaylistCommand, DeletePlaylistResponse>,
    IRequestHandler<UpdatePlaylistCommand, UpdatePlaylistResponse>,
    IRequestHandler<AddVideosToPlaylistCommand, AddVideosToPlaylistResponse>

{
    private readonly VideomaticDbContext _dbContext;
    private readonly IMapper _mapper;

    public PlaylistCommandsHandler(VideomaticDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<CreatePlaylistResponse> Handle(CreatePlaylistCommand request, CancellationToken cancellationToken = default)
    {
        PlaylistDb dbPlaylist = _mapper.Map<CreatePlaylistCommand, PlaylistDb>(request);

        var entry = _dbContext.Add(dbPlaylist);
        var res = await _dbContext.SaveChangesAsync(cancellationToken);

        //_dbContext.ChangeTracker.Clear();

        return new CreatePlaylistResponse(Id: entry.Entity.Id);
    }

    public async Task<UpdatePlaylistResponse> Handle(UpdatePlaylistCommand request, CancellationToken cancellationToken = default)
    {
        var newValue = _mapper.Map<UpdatePlaylistCommand, PlaylistDb>(request);

        var attached = await _dbContext.Playlists
            .AsTracking()        
            .SingleAsync(x => x.Id == request.Id, cancellationToken);

        var entry = _dbContext.Entry(attached);
        entry.State = EntityState.Detached;
        //foreach (var item in attached.Videos.ToList())
        //    _dbContext.Entry(item).State = EntityState.Detached;

        var entry2 = _dbContext.Attach(newValue);

        var cnt = await _dbContext.SaveChangesAsync(cancellationToken);

        return new UpdatePlaylistResponse(request.Id, cnt > 0);
    }

    public Task<DeletePlaylistResponse> Handle(DeletePlaylistCommand request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<AddVideosToPlaylistResponse> Handle(AddVideosToPlaylistCommand request, CancellationToken cancellationToken = default)
    {
        var dupVideoIdsQuery = _dbContext.PlaylistVideos
            .Where(x => x.PlaylistId==request.PlaylistId && request.VideoIds.Contains(x.VideoId))
            .Select(x => x.VideoId);

        var notLinked = request.VideoIds.Except(dupVideoIdsQuery)
            .ToArray();

        foreach (var newId in notLinked)
        {
            var newRef = new PlaylistDbVideoDb()
            { 
                PlaylistId = request.PlaylistId,    
                VideoId = newId
            };
            _dbContext.Add(newRef);
        }

        var cnt = await _dbContext.SaveChangesAsync(cancellationToken);

        return new AddVideosToPlaylistResponse(request.PlaylistId, notLinked);
    }
}
