namespace Company.Videomatic.Application.Handlers.Artifacts.Commands;

public class UpdateArtifactHandler : UpdateAggregateRootHandler<UpdateArtifactCommand, Artifact>
{
    public UpdateArtifactHandler(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
    {
    }
}
