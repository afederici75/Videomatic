using SharedKernel.CQRS.Commands;

namespace Application.Features.Playlists.Commands;

public record DeletePlaylistCommand(PlaylistId Id) : IRequest<Result>;

internal class DeletePlaylistCommandValidator : AbstractValidator<DeletePlaylistCommand>
{
    public DeletePlaylistCommandValidator()
    {
       RuleFor(x => (int)x.Id).GreaterThan(0);
    }
}