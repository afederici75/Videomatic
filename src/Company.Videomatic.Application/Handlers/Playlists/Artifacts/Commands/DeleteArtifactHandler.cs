namespace Company.Videomatic.Application.Handlers.Artifacts.Commands;

public class DeleteArtifactHandler : DeleteAggregateRootHandler<DeleteArtifactCommand, Artifact>
{
    public DeleteArtifactHandler(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
    {
    }
}
