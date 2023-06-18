namespace Company.Videomatic.Application.Features.Playlists;

public record CreatePlaylistCommand(string Name, string? Description) : IRequest<Playlist>;

public class CreatePlaylistCommandValidator : AbstractValidator<CreatePlaylistCommand>
{
    public CreatePlaylistCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}