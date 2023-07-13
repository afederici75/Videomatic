using Ardalis.Specification;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Artifacts.Commands;

public class CreateArtifactHandler : CreateEntityHandlerBase<CreateArtifactCommand, Result<CreateArtifactResponse>, Artifact>
{    
    public CreateArtifactHandler(IRepository<Artifact> repository,
                                 IMapper mapper) : base(repository, mapper)
    { }

    protected override Result<CreateArtifactResponse> CreateResponseFor(Artifact entity)
        => new CreateArtifactResponse(entity.Id);    
}
