namespace Company.Videomatic.Application.Handlers.Artifacts.Commands;

public class DeleteArtifactHandler : DeleteEntityHandlerBase<DeleteArtifactCommand, DeleteArtifactResponse, Artifact, ArtifactId>
{
    public DeleteArtifactHandler(IRepository<Artifact> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    protected override ArtifactId GetIdOfRequest(DeleteArtifactCommand request)
        => new ArtifactId(request.Id);

    protected override DeleteArtifactResponse CreateResponseFor(ArtifactId entityId, bool wasDeleted)
        => new DeleteArtifactResponse(entityId, wasDeleted);

}
