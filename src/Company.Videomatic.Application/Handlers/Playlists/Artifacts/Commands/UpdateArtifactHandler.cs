namespace Company.Videomatic.Application.Handlers.Artifacts.Commands;

public class UpdateArtifactHandler : UpdateEntityHandlerBase<UpdateArtifactCommand, UpdateArtifactResponse, Artifact, ArtifactId>
{
    public UpdateArtifactHandler(IRepository<Artifact> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    protected override UpdateArtifactResponse CreateResponseFor(ArtifactId updatedEntityId, bool wasUpdated)
       => new(updatedEntityId, wasUpdated);

    protected override ArtifactId GetIdOfRequest(UpdateArtifactCommand request)
       => request.Id;
}
