namespace Company.Videomatic.Application.Features.Playlists.Commands;

public record UpdatePlaylistCommand(long Id, string Name, string? Description) : IRequest<UpdatedResponse>;

internal class UpdatePlaylistCommandValidator : AbstractValidator<UpdatePlaylistCommand>
{
    public UpdatePlaylistCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
    }
}