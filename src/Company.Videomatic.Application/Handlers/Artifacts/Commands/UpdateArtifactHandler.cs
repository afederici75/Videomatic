namespace Company.Videomatic.Application.Handlers.Artifacts.Commands;

public class UpdateArtifactHandler : UpdateAggregateRootHandler<UpdateArtifactCommand, Artifact, ArtifactId>
{
    public UpdateArtifactHandler(IRepository<Artifact> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    protected override ArtifactId ConvertIdOfRequest(UpdateArtifactCommand request) => request.Id;
}
