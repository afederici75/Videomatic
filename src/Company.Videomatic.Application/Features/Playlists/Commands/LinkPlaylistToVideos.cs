using System.Runtime.CompilerServices;

namespace Company.Videomatic.Application.Features.Playlists.Commands;

public record LinkPlaylistToVideosCommand(long Id, IEnumerable<long> VideoIds) : IRequest<Result<int>>
{
    public LinkPlaylistToVideosCommand(PlaylistId Id, IEnumerable<VideoId> VideoIds) : this(Id, VideoIds.Select(x => (long)x)) { }
};

internal class LinkPlaylistToVideosValidator : AbstractValidator<LinkPlaylistToVideosCommand>
{
    public LinkPlaylistToVideosValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.VideoIds).NotEmpty();
        RuleForEach(x => x.VideoIds).GreaterThan(0);
    }
}