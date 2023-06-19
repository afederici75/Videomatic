namespace Company.Videomatic.Application.Features.Playlists.Commands;

public record LinkVideosAndPlaylistsCommand(long PlaylistId, long[] VideoIds) : IRequest<LinkVideosAndPlaylistsResponse>;

public record LinkVideosAndPlaylistsResponse(long PlaylistId, long[] VideoIds);

public class LinkVideosAndPlaylistsValidator : AbstractValidator<LinkVideosAndPlaylistsCommand>
{
    public LinkVideosAndPlaylistsValidator()
    {
        RuleFor(x => x.PlaylistId).GreaterThan(0);
        RuleFor(x => x.VideoIds).NotEmpty();
        RuleForEach(x => x.VideoIds).GreaterThan(0);
    }
}