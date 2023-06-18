﻿using Company.Videomatic.Application.Features.Playlists;
using Company.Videomatic.Domain.Model;
using Company.Videomatic.Infrastructure.Data.Model;
using MediatR;

namespace Company.Videomatic.Infrastructure.Data.Handlers;

public class PlaylistCommandsHandler :
    IRequestHandler<CreatePlaylistCommand, Playlist>,
    IRequestHandler<DeletePlaylistCommand, DeletePlaylistResponse>,
    IRequestHandler<UpdatePlaylistCommand, UpdatePlaylistResponse>

{
    private readonly VideomaticDbContext _dbContext;
    private readonly IMapper _mapper;

    public PlaylistCommandsHandler(VideomaticDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Playlist> Handle(CreatePlaylistCommand request, CancellationToken cancellationToken = default)
    {
        PlaylistDb dbPlaylist = _mapper.Map<CreatePlaylistCommand, PlaylistDb>(request);

        var entry = _dbContext.Add(dbPlaylist);
        var res = await _dbContext.SaveChangesAsync(cancellationToken);

        //_dbContext.ChangeTracker.Clear();

        return _mapper.Map<PlaylistDb, Playlist>(entry.Entity);
    }

    public Task<DeletePlaylistResponse> Handle(DeletePlaylistCommand request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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
}
