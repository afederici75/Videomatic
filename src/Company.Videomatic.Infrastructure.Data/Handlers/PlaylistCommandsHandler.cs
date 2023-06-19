using Company.Videomatic.Application.Features.Playlists.Commands;
using MediatR;
using System.Security.Cryptography;

namespace Company.Videomatic.Infrastructure.Data.Handlers;

public class PlaylistCommandsHandler :
    IRequestHandler<CreatePlaylistCommand, CreatePlaylistResponse>,
    IRequestHandler<DeletePlaylistCommand, DeletePlaylistResponse>,
    IRequestHandler<UpdatePlaylistCommand, UpdatePlaylistResponse>,
    IRequestHandler<LinkVideosAndPlaylistsCommand, LinkVideosAndPlaylistsResponse>

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
        Playlist dbPlaylist = _mapper.Map<CreatePlaylistCommand, Playlist>(request);

        var entry = _dbContext.Add(dbPlaylist);
        var res = await _dbContext.SaveChangesAsync(cancellationToken);

        //_dbContext.ChangeTracker.Clear();

        return new CreatePlaylistResponse(Id: entry.Entity.Id);
    }

    public async Task<UpdatePlaylistResponse> Handle(UpdatePlaylistCommand request, CancellationToken cancellationToken = default)
    {
        var newValue = _mapper.Map<UpdatePlaylistCommand, Playlist>(request);

        var playlistDb = await _dbContext.Playlists
            .AsTracking()        
            .SingleAsync(x => x.Id == request.Id, cancellationToken);

        _mapper.Map(request, playlistDb);

        var cnt = await _dbContext.SaveChangesAsync(cancellationToken);

        return new UpdatePlaylistResponse(request.Id, cnt > 0);
    }

    public async Task<DeletePlaylistResponse> Handle(DeletePlaylistCommand request, CancellationToken cancellationToken = default)
    {
        int cnt = await _dbContext
            .Playlists
            .Where(x => x.Id==request.Id).
            ExecuteDeleteAsync(cancellationToken);

        return new DeletePlaylistResponse(request.Id, cnt > 0);
    }

    public async Task<LinkVideosAndPlaylistsResponse> Handle(LinkVideosAndPlaylistsCommand request, CancellationToken cancellationToken = default)
    {
        var dupVideoIdsQuery = _dbContext.PlaylistVideos
            .Where(x => x.PlaylistId==request.PlaylistId && request.VideoIds.Contains(x.VideoId))
            .Select(x => x.VideoId);

        var notLinked = request.VideoIds.Except(dupVideoIdsQuery)
            .ToArray();

        foreach (var newId in notLinked)
        {
            var newRef = new PlaylistVideo()
            { 
                PlaylistId = request.PlaylistId,    
                VideoId = newId
            };
            _dbContext.Add(newRef);
        }

        var cnt = await _dbContext.SaveChangesAsync(cancellationToken);

        return new LinkVideosAndPlaylistsResponse(request.PlaylistId, notLinked);
    }
}
