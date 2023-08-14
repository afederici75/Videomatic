using SharedKernel.CQRS.Commands;

namespace Application.Features.Playlists.Commands;

public record UpdatePlaylistCommand(int Id, string Name, string? Description) : UpdateEntityCommand<Playlist>(Id);

public record UpdatePlaylistResponse(int Id, bool WasUpdated);

internal class UpdatePlaylistCommandValidator : AbstractValidator<UpdatePlaylistCommand>
{
    public UpdatePlaylistCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
    }
}