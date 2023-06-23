namespace Company.Videomatic.Application.Features.Videos.Commands;

public record LinkVideosToPlaylistCommand(long PlaylistId, long[] VideoIds) : IRequest<LinkVideosToPlaylistResponse>;

public record LinkVideosToPlaylistResponse(long PlaylistId, long[] VideoIds);

public class LinkVideosToPlaylistValidator : AbstractValidator<LinkVideosToPlaylistCommand>
{
    public LinkVideosToPlaylistValidator()
    {
        RuleFor(x => x.PlaylistId).NotEmpty();
        RuleFor(x => x.PlaylistId).GreaterThan(0);

        RuleFor(x => x.VideoIds).NotEmpty();
        RuleForEach(x => x.VideoIds).GreaterThan(0);
    }
}