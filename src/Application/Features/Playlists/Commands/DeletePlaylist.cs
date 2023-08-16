using SharedKernel.CQRS.Commands;

namespace Application.Features.Playlists.Commands;

public class DeletePlaylistCommand(PlaylistId Id) : IRequest<Result>
{ 
    public PlaylistId Id { get; } = Id;

    internal class DeletePlaylistCommandValidator : AbstractValidator<DeletePlaylistCommand>
    {
        public DeletePlaylistCommandValidator()
        {
            RuleFor(x => (int)x.Id).GreaterThan(0);
        }
    }
}