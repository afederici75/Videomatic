using SharedKernel.CQRS.Commands;

namespace Application.Features.Playlists.Commands;

public class UpdatePlaylistCommand(PlaylistId id, string name, string? description) : IRequest<Result<Playlist>>
{ 
    public PlaylistId Id { get; } = id; 
    public string Name { get; } = name;
    public string? Description { get; } = description;

    internal class Validator : AbstractValidator<UpdatePlaylistCommand>
    {
        public Validator()
        {
            RuleFor(x => (int)x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
    internal class Handler(IRepository<Playlist> repository, IMapper mapper) : UpdateEntityHandler<UpdatePlaylistCommand, Playlist, PlaylistId>(repository, mapper)
    {
    }

}