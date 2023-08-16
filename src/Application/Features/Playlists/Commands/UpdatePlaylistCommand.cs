using SharedKernel.CQRS.Commands;

namespace Application.Features.Playlists.Commands;

public class UpdatePlaylistCommand(PlaylistId id, string name, string? description) : IRequest<Result<Playlist>>
{ 
    public PlaylistId Id { get; } = id; 
    public string Name { get; } = name;
    public string? Description { get; } = description;

    internal class UpdatePlaylistCommandValidator : AbstractValidator<UpdatePlaylistCommand>
    {
        public UpdatePlaylistCommandValidator()
        {
            RuleFor(x => (int)x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}