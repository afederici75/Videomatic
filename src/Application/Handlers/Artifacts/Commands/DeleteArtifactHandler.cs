using SharedKernel.CQRS.Commands;

namespace Application.Handlers.Artifacts.Commands;

public class DeleteArtifactHandler : DeleteEntityHandler<DeleteArtifactCommand, Artifact, ArtifactId>
{
    public DeleteArtifactHandler(IRepository<Artifact> repository, IMapper mapper) : base(repository, mapper)
    {
    }   
}
