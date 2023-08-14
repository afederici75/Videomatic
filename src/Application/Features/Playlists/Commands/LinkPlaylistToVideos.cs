using System.Runtime.CompilerServices;
using Domain.Videos;

namespace Application.Features.Playlists.Commands;

public record LinkPlaylistToVideosCommand(int Id, IEnumerable<int> VideoIds) : IRequest<Result<int>>
{
    public LinkPlaylistToVideosCommand(PlaylistId Id, IEnumerable<VideoId> VideoIds) : this(Id, VideoIds.Select(x => (int)x)) { }
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