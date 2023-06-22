namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Commands;

public class LinkVideosAndPlaylistsHandler : BaseRequestHandler<LinkVideosToPlaylistsCommand, LinkVideosToPlaylistsResponse>
{
    public LinkVideosAndPlaylistsHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<LinkVideosToPlaylistsResponse> Handle(LinkVideosToPlaylistsCommand request, CancellationToken cancellationToken = default)
    {
        // PlayLists: 1, 2, 3
        // Videos: 22, 35
        // ---------------------------------
        // Result: [1,22] [1,35], [2,22] [2,35], [3,22] [3,35]

        throw new NotImplementedException();
        //var dupVideoIdsQuery = DbContext.PlaylistVideos
        //  .Where(x => request.PlaylistId.Contains(x.PlaylistId) && request.VideoIds.Contains(x.VideoId))
        //  .Select(x => x.VideoId);
        //
        //var notLinked = request.VideoIds.Except(dupVideoIdsQuery)
        //    .ToArray();
        //
        //foreach (var newId in notLinked)
        //{
        //    var newRef = new PlaylistVideo()
        //    {
        //        PlaylistId = request.PlaylistId,
        //        VideoId = newId
        //    };
        //    DbContext.Add(newRef);
        //}
        //
        //var cnt = await DbContext.SaveChangesAsync(cancellationToken);
        //
        //return new LinkVideosToPlaylistsResponse(request.PlaylistId, notLinked);
    }
}
