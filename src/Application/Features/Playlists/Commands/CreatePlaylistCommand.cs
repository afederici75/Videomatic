namespace Application.Features.Playlists.Commands;

public class CreatePlaylistCommand(
    string name,
    string? description = null) : IRequest<Result<Playlist>>
{ 
    public string Name { get; } = name;
    public string? Description { get; } = description;

    #region Validator

    internal class Validator : AbstractValidator<CreatePlaylistCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    #endregion

    #region Handler

    internal class Handler : CreateEntityHandler<CreatePlaylistCommand, Playlist>
    {
        public Handler(IRepository<Playlist> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }

    #endregion
}
