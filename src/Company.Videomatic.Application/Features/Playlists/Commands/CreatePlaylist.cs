using Company.Videomatic.Application.Features.DataAccess;

namespace Company.Videomatic.Application.Features.Playlists.Commands;

public record CreatePlaylistCommand(string Name, string? Description) : IRequest<CreatedResponse>;

public class CreatePlaylistCommandValidator : AbstractValidator<CreatePlaylistCommand>
{
    public CreatePlaylistCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();        
    }
}