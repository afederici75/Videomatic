namespace Application.Handlers.Playlists.Commands;

public class LinkVideoToPlaylistsHandler : IRequestHandler<LinkPlaylistToVideosCommand, Result<int>>
{
    public LinkVideoToPlaylistsHandler(
        IRepository<Playlist> repository)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public IRepository<Playlist> Repository { get; }

    public async Task<Result<int>> Handle(LinkPlaylistToVideosCommand request, CancellationToken cancellationToken = default)
    {
        var cnt = await Repository.LinkPlaylistToVideos(request.Id, request.VideoIds.Select(x => new VideoId(x)), cancellationToken);

        return new Result<int>(cnt);
    }

}