namespace Company.Videomatic.Application.Features.Playlists.Commands;

public record DeletePlaylistCommand(long Id) : IDeleteCommand<Playlist>;

internal class DeletePlaylistCommandValidator : AbstractValidator<DeletePlaylistCommand>
{
    public DeletePlaylistCommandValidator()
    {
       RuleFor(x => x.Id).GreaterThan(0);
    }
}