namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Commands;

public class LinkVideosAndPlaylistsHandler : BaseRequestHandler<LinkVideosToPlaylistCommand, LinkVideosToPlaylistResponse>
{
    public LinkVideosAndPlaylistsHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<LinkVideosToPlaylistResponse> Handle(LinkVideosToPlaylistCommand request, CancellationToken cancellationToken = default)
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

        return new LinkVideosToPlaylistResponse(request.PlaylistId, notLinked);
    }
}
