namespace Application.Features.Playlists.Commands;

public class CreatePlaylistCommand(string Name,
                                    string? Description = null) : IRequest<Result<Playlist>>
{ 
    public string Name { get; } = Name;
    public string? Description { get; } = Description;


    internal class CreatePlaylistCommandValidator : AbstractValidator<CreatePlaylistCommand>
    {
        public CreatePlaylistCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
