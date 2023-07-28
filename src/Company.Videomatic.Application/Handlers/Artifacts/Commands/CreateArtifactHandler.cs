namespace Company.Videomatic.Application.Handlers.Artifacts.Commands;

public class CreateArtifactHandler : CreateAggregateRootHandler<CreateArtifactCommand, Artifact>
{
    public CreateArtifactHandler(IRepository<Artifact> repository, IMapper mapper) : base(repository, mapper)
    {
    }    
}