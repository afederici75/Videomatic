using SharedKernel.CQRS.Commands;

namespace Application.Features.Playlists.Commands;

public record UpdatePlaylistCommand(PlaylistId Id, string Name, string? Description) : IRequest<Result<Playlist>>;

public record UpdatePlaylistResponse(int Id, bool WasUpdated);

internal class UpdatePlaylistCommandValidator : AbstractValidator<UpdatePlaylistCommand>
{
    public UpdatePlaylistCommandValidator()
    {
        RuleFor(x => (int)x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
    }
}