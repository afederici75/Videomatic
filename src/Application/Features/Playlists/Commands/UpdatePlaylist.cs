using SharedKernel.CQRS.Commands;

namespace Application.Features.Playlists.Commands;

public class UpdatePlaylistCommand(PlaylistId Id, string Name, string? Description) : IRequest<Result<Playlist>>
{ 
    public PlaylistId Id { get; } = Id; 
    public string Name { get; } = Name;
    public string? Description { get; } = Description;

    internal class UpdatePlaylistCommandValidator : AbstractValidator<UpdatePlaylistCommand>
    {
        public UpdatePlaylistCommandValidator()
        {
            RuleFor(x => (int)x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}