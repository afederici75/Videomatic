using Ardalis.Specification;

namespace Company.Videomatic.Application.Handlers.Artifacts.Commands;

public class NewCreateArtifactHandler : IRequestHandler<CreateArtifactCommand, Result<CreateArtifactResponse>>
{
    public NewCreateArtifactHandler(IServiceProvider serviceProvider, IMapper mapper)
    {
        ServiceProvider = serviceProvider;
        //Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        CreateArtifactCommand cmd = new CreateArtifactCommand(1, "Name", "Type", "text...");
        if (cmd is ICRUDCommand)
        {
            //long id = cmd.Id;
        }


    }

    protected IServiceProvider ServiceProvider { get; }

    protected IMapper Mapper { get; }

    public Task<Result<CreateArtifactResponse>> Handle(CreateArtifactCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
