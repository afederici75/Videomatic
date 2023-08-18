using SharedKernel.CQRS.Commands;

namespace Application.Features.Playlists.Commands;

public class DeletePlaylistCommand(PlaylistId id) : IRequest<Result>
{ 
    public PlaylistId Id { get; } = id;

    internal class Validator : AbstractValidator<DeletePlaylistCommand>
    {
        public Validator()
        {
            RuleFor(x => (int)x.Id).GreaterThan(0);
        }
    }


    internal class Handler(IMyRepository<Playlist> repository, IMapper mapper) : DeleteEntityHandler<DeletePlaylistCommand, Playlist, PlaylistId>(repository, mapper)
    {
    }

}