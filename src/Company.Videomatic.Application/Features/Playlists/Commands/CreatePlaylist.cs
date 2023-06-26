using Company.Videomatic.Application.Features.DataAccess;
using FluentValidation.TestHelper;

namespace Company.Videomatic.Application.Features.Playlists.Commands;

public record CreatePlaylistCommand(string Name, string? Description = null) : IRequest<CreatedResponse>;

internal class CreatePlaylistCommandValidator : AbstractValidator<CreatePlaylistCommand>
{
    public CreatePlaylistCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();        
    }
}