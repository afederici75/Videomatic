namespace Application.Features.Playlists.Commands;

public class LinkPlaylistToVideosCommand(PlaylistId Id, IEnumerable<VideoId> VideoIds) : IRequest<Result<int>>
{ 
    public PlaylistId Id { get; } = Id;
    public IEnumerable<VideoId> VideoIds { get; } = VideoIds;
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