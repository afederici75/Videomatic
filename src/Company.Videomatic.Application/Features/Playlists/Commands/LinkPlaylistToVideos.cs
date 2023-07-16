namespace Company.Videomatic.Application.Features.Playlists.Commands;

public record LinkPlaylistToVideosCommand(long Id, long[] VideoIds) : IRequest<Result<int>>;

internal class LinkPlaylistToVideosValidator : AbstractValidator<LinkPlaylistToVideosCommand>
{
    public LinkPlaylistToVideosValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.VideoIds).NotEmpty();
        RuleForEach(x => x.VideoIds).GreaterThan(0);
    }
}