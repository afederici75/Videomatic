namespace Company.Videomatic.Application.Features.Videos.Commands;

public record LinkVideosToPlaylistsCommand(long[] PlaylistId, long[] VideoIds) : IRequest<LinkVideosToPlaylistsResponse>;

public record LinkVideosToPlaylistsResponse(long[] PlaylistId, long[] VideoIds);

public class LinkVideosAndPlaylistsValidator : AbstractValidator<LinkVideosToPlaylistsCommand>
{
    public LinkVideosAndPlaylistsValidator()
    {
        RuleFor(x => x.PlaylistId).NotEmpty();
        RuleForEach(x => x.PlaylistId).GreaterThan(0);

        RuleFor(x => x.VideoIds).NotEmpty();
        RuleForEach(x => x.VideoIds).GreaterThan(0);
    }
}