using Company.SharedKernel.Common.CQRS;

namespace Company.Videomatic.Application.Features.Playlists.Commands;

public record CreatePlaylistCommand(string Name,
                                    string? Description = null) : CreateEntityCommand<Playlist>();

internal class CreatePlaylistCommandValidator : AbstractValidator<CreatePlaylistCommand>
{
    public CreatePlaylistCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();        
    }
}