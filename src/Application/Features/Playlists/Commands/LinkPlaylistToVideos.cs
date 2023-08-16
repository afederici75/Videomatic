namespace Application.Features.Playlists.Commands;

public record LinkPlaylistToVideosCommand(PlaylistId Id, IEnumerable<int> VideoIds) : IRequest<Result<int>>;

internal class LinkPlaylistToVideosValidator : AbstractValidator<LinkPlaylistToVideosCommand>
{
    public LinkPlaylistToVideosValidator()
    {
        RuleFor(x => (int)x.Id).GreaterThan(0);

        RuleFor(x => x.VideoIds).NotEmpty();
        RuleForEach(x => x.VideoIds).GreaterThan(0);
    }
}