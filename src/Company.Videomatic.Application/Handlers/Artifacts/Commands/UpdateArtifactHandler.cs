namespace Company.Videomatic.Application.Handlers.Artifacts.Commands;

public class UpdateArtifactHandler : UpdateAggregateRootHandler<UpdateArtifactCommand, Artifact, ArtifactId>
{
    public UpdateArtifactHandler(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
    {
    }

    protected override ArtifactId ConvertIdOfRequest(UpdateArtifactCommand request) => request.Id;
}
