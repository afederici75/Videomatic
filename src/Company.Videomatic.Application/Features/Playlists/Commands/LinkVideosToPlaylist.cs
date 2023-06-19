namespace Company.Videomatic.Application.Features.Playlists.Commands;

public record LinkVideosToPlaylistCommand(long PlaylistId, long[] VideoIds) : IRequest<LinkVideosToPlaylistResponse>;

public record LinkVideosToPlaylistResponse(long PlaylistId, long[] VideoIds);

public class LinkVideosAndPlaylistsValidator : AbstractValidator<LinkVideosToPlaylistCommand>
{
    public LinkVideosAndPlaylistsValidator()
    {
        RuleFor(x => x.PlaylistId).GreaterThan(0);
        RuleFor(x => x.VideoIds).NotEmpty();
        RuleForEach(x => x.VideoIds).GreaterThan(0);
    }
}