namespace Application.Features.Artifacts.Commands;

public class DeleteArtifactCommand(ArtifactId id) : IRequest<Result>
{
    public ArtifactId Id { get; } = id;

    internal class Validator : AbstractValidator<DeleteArtifactCommand>
    {
        public Validator()
        {
            RuleFor(x => (int)x.Id).GreaterThan(0);
        }
    }
    internal class Handler(IMyRepository<Artifact> repository, IMapper mapper) : DeleteEntityHandler<DeleteArtifactCommand, Artifact, ArtifactId>(repository, mapper)
    {
    }

}