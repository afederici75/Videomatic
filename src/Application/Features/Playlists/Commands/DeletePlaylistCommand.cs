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


    internal class Handler : DeleteEntityHandler<DeletePlaylistCommand, Playlist, PlaylistId>
    {
        public Handler(IRepository<Playlist> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }

}