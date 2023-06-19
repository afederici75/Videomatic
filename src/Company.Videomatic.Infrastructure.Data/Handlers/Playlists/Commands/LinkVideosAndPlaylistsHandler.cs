namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Commands;

public class LinkVideosAndPlaylistsHandler : BaseRequestHandler<LinkVideosAndPlaylistsCommand, LinkVideosAndPlaylistsResponse>
{
    public LinkVideosAndPlaylistsHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<LinkVideosAndPlaylistsResponse> Handle(LinkVideosAndPlaylistsCommand request, CancellationToken cancellationToken = default)
    {
        var dupVideoIdsQuery = DbContext.PlaylistVideos
          .Where(x => x.PlaylistId == request.PlaylistId && request.VideoIds.Contains(x.VideoId))
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
            DbContext.Add(newRef);
        }

        var cnt = await DbContext.SaveChangesAsync(cancellationToken);

        return new LinkVideosAndPlaylistsResponse(request.PlaylistId, notLinked);
    }
}
