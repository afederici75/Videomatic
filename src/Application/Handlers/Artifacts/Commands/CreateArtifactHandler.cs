using SharedKernel.CQRS.Commands;

namespace Application.Handlers.Artifacts.Commands;

public class CreateArtifactHandler : CreateEntitytHandler<CreateArtifactCommand, Artifact>
{
    public CreateArtifactHandler(IRepository<Artifact> repository, IMapper mapper) : base(repository, mapper)
    {
    }    
}