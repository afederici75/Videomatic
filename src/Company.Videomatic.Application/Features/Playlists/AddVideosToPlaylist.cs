using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Videomatic.Application.Features.Playlists;

public record AddVideosToPlaylistCommand(long PlaylistId, long[] VideoIds) : IRequest<AddVideosToPlaylistResponse>;

public record AddVideosToPlaylistResponse(long PlaylistId, long[] VideoIds);

public class AddVideosToPlaylistValidator : AbstractValidator<AddVideosToPlaylistCommand>
{
    public AddVideosToPlaylistValidator()
    {
        RuleFor(x => x.PlaylistId).GreaterThan(0);
        RuleFor(x => x.VideoIds).NotEmpty();
        RuleForEach(x => x.VideoIds).GreaterThan(0);
    }
}