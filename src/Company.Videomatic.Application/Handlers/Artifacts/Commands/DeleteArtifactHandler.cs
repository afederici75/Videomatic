namespace Company.Videomatic.Application.Handlers.Artifacts.Commands;

public class DeleteArtifactHandler : DeleteAggregateRootHandler<DeleteArtifactCommand, Artifact, ArtifactId>
{
    public DeleteArtifactHandler(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
    {
    }

    protected override ArtifactId ConvertIdOfRequest(DeleteArtifactCommand request) => request.Id;
}
