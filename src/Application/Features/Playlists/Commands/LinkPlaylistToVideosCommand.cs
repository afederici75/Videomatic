namespace Application.Features.Playlists.Commands;

public class LinkPlaylistToVideosCommand(PlaylistId id, IEnumerable<VideoId> videoIds) : IRequest<Result<int>>
{
    public PlaylistId Id { get; } = id;
    public IEnumerable<VideoId> VideoIds { get; } = videoIds;

    #region Validator

    internal class Validator : AbstractValidator<LinkPlaylistToVideosCommand>
    {
        public Validator()
        {
            RuleFor(x => (int)x.Id).GreaterThan(0);

            RuleFor(x => x.VideoIds).NotEmpty();
            //RuleForEach(x => x.VideoIds.Select(v => v.Value)).GreaterThan(0);
        }
    }

    #endregion

    #region Handler

    internal class Handler : IRequestHandler<LinkPlaylistToVideosCommand, Result<int>>
    {
        public Handler(
            IRepository<Playlist> repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IRepository<Playlist> Repository { get; }

        public async Task<Result<int>> Handle(LinkPlaylistToVideosCommand request, CancellationToken cancellationToken = default)
        {
            var cnt = await Repository.LinkPlaylistToVideos(request.Id, request.VideoIds, cancellationToken);

            return new Result<int>(cnt);
        }
    }

    #endregion
}