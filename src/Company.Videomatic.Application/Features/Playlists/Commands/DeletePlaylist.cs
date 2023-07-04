namespace Company.Videomatic.Application.Features.Playlists.Commands;

public record DeletePlaylistCommand(long Id) : IRequest<DeletePlaylistResponse>, ICommandWithEntityId;

public record DeletePlaylistResponse(long Id, bool deleted);

internal class DeletePlaylistCommandValidator : AbstractValidator<DeletePlaylistCommand>
{
    public DeletePlaylistCommandValidator()
    {
       RuleFor(x => x.Id).GreaterThan(0);
    }
}