namespace Company.Videomatic.Application.Features.Playlists;

public record CreatePlaylistCommand(string Name, string? Description) : IRequest<CreatePlaylistResponse>;

public record CreatePlaylistResponse(long Id);

public class CreatePlaylistCommandValidator : AbstractValidator<CreatePlaylistCommand>
{
    public CreatePlaylistCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}