namespace Company.Videomatic.Application.Handlers.Artifacts.Commands;

public class CreateArtifactHandler : CreateAggregateRootHandler<CreateArtifactCommand, Artifact>,
    IRequestHandler<CreateArtifactCommand, Result<long>>
{
    public CreateArtifactHandler(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
    {
    }    
}