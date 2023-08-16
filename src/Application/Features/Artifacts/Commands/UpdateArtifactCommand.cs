using SharedKernel.CQRS.Commands;

namespace Application.Features.Artifacts.Commands;

public class UpdateArtifactCommand(ArtifactId id, string name, string? text) : IRequest<Result<Artifact>>
{
    public ArtifactId Id { get; } = id;
    public string Name { get; } = name;
    public string? Text { get; } = text;

    internal class Validator : AbstractValidator<UpdateArtifactCommand>
    {
        public Validator()
        {
            RuleFor(x => (int)x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    internal class Handler : UpdateEntityHandler<UpdateArtifactCommand, Artifact, ArtifactId>
    {
        public Handler(IRepository<Artifact> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }

}