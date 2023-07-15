namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public class LinkVideoToPlaylistsHandler : IRequestHandler<LinkPlaylistToVideosCommand, Result<int>>
{
    private readonly IPlaylistService _playlistService;
    private readonly IMapper _mapper;

    public LinkVideoToPlaylistsHandler(
        IPlaylistService playlistService,
        IMapper mapper)
    {
        _playlistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<int>> Handle(LinkPlaylistToVideosCommand request, CancellationToken cancellationToken = default)
    {
        var cnt = await _playlistService.LinkToPlaylists(request.Id, request.VideoIds.Select(x => new VideoId(x)));

        return new Result<int>(cnt);
    }

}