using Company.Videomatic.Application.Features.DataAccess;

namespace Company.Videomatic.Application.Features.Playlists.Commands;


public record DeletePlaylistCommand(long Id) : IRequest<DeletedResponse>;

internal class DeletePlaylistCommandValidator : AbstractValidator<DeletePlaylistCommand>
{
    public DeletePlaylistCommandValidator()
    {
       RuleFor(x => x.Id).GreaterThan(0);
    }
}

/// <summary>
/// This event is published when a video is deleted.
/// </summary>
//public record PlaylistDeletedEvent(Playlist Item) : INotification;
