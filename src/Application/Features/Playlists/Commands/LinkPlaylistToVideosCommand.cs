namespace Application.Features.Playlists.Commands;

public class LinkPlaylistToVideosCommand(PlaylistId id, IEnumerable<VideoId> videoIds) : IRequest<Result<int>>
{ 
    public PlaylistId Id { get; } = id;
    public IEnumerable<VideoId> VideoIds { get; } = videoIds;
}

internal class LinkPlaylistToVideosValidator : AbstractValidator<LinkPlaylistToVideosCommand>
{
    public LinkPlaylistToVideosValidator()
    {
        RuleFor(x => (int)x.Id).GreaterThan(0);

        RuleFor(x => x.VideoIds).NotEmpty();
        //RuleForEach(x => x.VideoIds.Select(v => v.Value)).GreaterThan(0);
    }
}