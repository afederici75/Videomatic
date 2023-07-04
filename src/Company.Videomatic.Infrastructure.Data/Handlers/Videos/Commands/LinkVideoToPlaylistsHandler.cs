namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public class LinkVideoToPlaylistsHandler : IRequestHandler<LinkVideoToPlaylistsCommand, LinkVideoToPlaylistsResponse>
{
    private readonly IVideoService _videoService;
    private readonly IMapper _mapper;

    public LinkVideoToPlaylistsHandler(
        IVideoService videoService,
        IMapper mapper)
    {
        _videoService = videoService ?? throw new ArgumentNullException(nameof(videoService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<LinkVideoToPlaylistsResponse> Handle(LinkVideoToPlaylistsCommand request, CancellationToken cancellationToken = default)
    {
        PlaylistId[] plIds = request.PlaylistIds.Select(x => new PlaylistId(x)).ToArray();// TODO: a bit much

        var cnt = await _videoService.LinkToPlaylists(request.Id, plIds);

        return new LinkVideoToPlaylistsResponse(request.Id, cnt);
    }

}