namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Commands;

public class LinkVideosAndPlaylistsHandler : BaseRequestHandler<LinkVideosToPlaylistCommand, LinkVideosToPlaylistResponse>
{
    public LinkVideosAndPlaylistsHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    //public async Task<LinkVideosToPlaylistResponse> Handle3(LinkVideosToPlaylistCommand request, CancellationToken cancellationToken)
    //{
    //    var dupVideoIdsQuery = DbContext.Playlists
    //      .Where(x => request.VideoIds.Contains(x.VideoId))
    //      .Select(x => x.VideoId);

    //    var notLinked = request.VideoIds.Except(dupVideoIdsQuery)
    //        .ToArray();

    //    foreach (var newId in notLinked)
    //    {
    //        var newRef = new PlaylistVideo(request.PlaylistId, newId);

    //        DbContext.Add(newRef);
    //    }

    //    var cnt = await DbContext.SaveChangesAsync(cancellationToken);

    //    return new LinkVideosToPlaylistResponse(request.PlaylistId, notLinked);
    //}

    public async override Task<LinkVideosToPlaylistResponse> Handle(LinkVideosToPlaylistCommand request, CancellationToken cancellationToken = default)
    {
        // PlayListId: 1
        // VideoIds: 22, 35
        // ---------------------------------
        // Result: [1,22] [1,35]

        //DbContext.ChangeTracker.Clear();        

        // Gathers the videoIds that are already linked to the playlist
        var dupVideoIds = await DbContext.PlaylistVideos
            .AsNoTracking()
            .Where(x => x.PlaylistId==request.PlaylistId && request.VideoIds.Contains(x.VideoId))
            .Select(x => x.VideoId)
          .ToListAsync(cancellationToken);

        var notLinked = request.VideoIds.Except(dupVideoIds);
        
        Playlist playlist = await DbContext.Playlists
            .Where(x => x.Id == request.PlaylistId)
            .Include(x => x.PlaylistVideos)
            .SingleAsync(cancellationToken);
        
        foreach (var newId in notLinked)
        {
            playlist.AddPlaylistVideo(newId);
        }
        
        var cnt = await DbContext.CommitChangesAsync(cancellationToken);
        return new LinkVideosToPlaylistResponse(request.PlaylistId, notLinked.ToArray());
        //
    }
}
