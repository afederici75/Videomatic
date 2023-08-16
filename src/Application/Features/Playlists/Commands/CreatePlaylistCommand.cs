namespace Application.Features.Playlists.Commands;

public class CreatePlaylistCommand(string name,
                                    string? description = null) : IRequest<Result<Playlist>>
{ 
    public string Name { get; } = name;
    public string? Description { get; } = description;


    internal class CreatePlaylistCommandValidator : AbstractValidator<CreatePlaylistCommand>
    {
        public CreatePlaylistCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
