using System.Security.Cryptography;
using Company.Videomatic.Domain.Entities.PlaylistAggregate;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Commands;

public class LinkVideosAndPlaylistsHandler : BaseRequestHandler<LinkVideosToPlaylistCommand, LinkVideosToPlaylistResponse>
{
    public LinkVideosAndPlaylistsHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }


    public async override Task<LinkVideosToPlaylistResponse> Handle(LinkVideosToPlaylistCommand request, CancellationToken cancellationToken = default)
    {        
        IQueryable<Playlist> playlistQuery = DbContext.Playlists
            .Where(pl => pl.Id == request.PlaylistId);

        var playlist = await playlistQuery.SingleAsync(cancellationToken);

        //
        throw new NotImplementedException();
        //var alreadyLinkedVideoIdsQuery = 
        //    from pl in playlistQuery
        //    from plvid in pl.PlaylistVideos
        //    where request.VideoIds.Contains(plvid.VideoId)
        //    select plvid.VideoId.Value;
        //var dupIds = await alreadyLinkedVideoIdsQuery.ToArrayAsync(cancellationToken);
        //
        //var notLinkedIds = request.VideoIds.Except(dupIds);
        //
        //foreach (var id in notLinkedIds)
        //{
        //    playlist.AddVideo(id);            
        //}
        //
        //var cnt = await DbContext.CommitChangesAsync(cancellationToken);
        //
        //return new LinkVideosToPlaylistResponse(request.PlaylistId, notLinkedIds.ToArray());
    }

    //public async override Task<LinkVideosToPlaylistResponse> Handle(LinkVideosToPlaylistCommand request, CancellationToken cancellationToken = default)
    //{
    //    // PlayListId: 1
    //    // VideoIds: 22, 35
    //    // ---------------------------------
    //    // Result: [1,22] [1,35]
    //
    //    // Gathers the videoIds that are already linked to the playlist
    //    var currentVideoIds = await DbContext.PlaylistVideos
    //        .Where(x => x.PlaylistId==request.PlaylistId && request.VideoIds.Contains(x.VideoId))
    //        .Select(x => x.VideoId)
    //      .ToListAsync(cancellationToken);
    //
    //    var notLinked = request.VideoIds.Except(currentVideoIds.Select(x => x.Value));
    //    
    //    foreach (var newId in notLinked)
    //    {
    //        PlaylistVideo item = PlaylistVideo.Create(request.PlaylistId, newId);
    //        DbContext.Add(item);
    //    }
    //    
    //    var cnt = await DbContext.CommitChangesAsync(cancellationToken);
    //    return new LinkVideosToPlaylistResponse(request.PlaylistId, notLinked.ToArray());        
    //}
}