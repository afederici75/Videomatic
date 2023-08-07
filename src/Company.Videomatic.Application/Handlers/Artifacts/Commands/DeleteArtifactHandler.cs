using Company.SharedKernel.CQRS.Commands;

namespace Company.Videomatic.Application.Handlers.Artifacts.Commands;

public class DeleteArtifactHandler : DeleteEntityHandler<DeleteArtifactCommand, Artifact, ArtifactId>
{
    public DeleteArtifactHandler(IRepository<Artifact> repository, IMapper mapper) : base(repository, mapper)
    {
    }   
}
