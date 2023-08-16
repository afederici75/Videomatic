using SharedKernel.CQRS.Commands;

namespace Application.Features.Playlists.Commands;

public readonly record struct UpdatePlaylistCommand(PlaylistId Id, string Name, string? Description) : IRequest<Result<Playlist>>;

public readonly record struct UpdatePlaylistResponse(int Id, bool WasUpdated);

internal class UpdatePlaylistCommandValidator : AbstractValidator<UpdatePlaylistCommand>
{
    public UpdatePlaylistCommandValidator()
    {
        RuleFor(x => (int)x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
    }
}