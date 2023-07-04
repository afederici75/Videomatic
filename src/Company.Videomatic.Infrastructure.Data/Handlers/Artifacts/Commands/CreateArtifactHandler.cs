namespace Company.Videomatic.Infrastructure.Data.Handlers.Artifacts.Commands;

public class CreateArtifactHandler : CreateEntityHandlerBase<CreateArtifactCommand, CreateArtifactResponse, Artifact>
{    
    public CreateArtifactHandler(IRepository<Artifact> repository,
                                 IMapper mapper) : base(repository, mapper)
    { }

    protected override CreateArtifactResponse CreateResponseFor(Artifact entity)
        => new CreateArtifactResponse(Id: entity.Id);    
}
