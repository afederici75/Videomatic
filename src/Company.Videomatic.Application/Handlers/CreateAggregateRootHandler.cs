using Company.Videomatic.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Videomatic.Application.Handlers;

public abstract class CreateAggregateRootHandler<TCreateCommand, TAggregateRoot> : IRequestHandler<TCreateCommand, Result<TAggregateRoot>>
    where TCreateCommand : ICreateCommand<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot
{
    public CreateAggregateRootHandler(IServiceProvider serviceProvider, IMapper mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        
        var repoType = typeof(IRepository<TAggregateRoot>);
        Repository = (IRepository<TAggregateRoot>)ServiceProvider.GetRequiredService(repoType);        
    }

    protected IServiceProvider ServiceProvider { get; }
    protected IRepository<TAggregateRoot> Repository { get; }
    protected IMapper Mapper { get; }

    public async Task<Result<TAggregateRoot>> Handle(TCreateCommand request, CancellationToken cancellationToken)
    {
        var aggRoot = Mapper.Map<TCreateCommand, TAggregateRoot>(request);
        
        var result = await Repository.AddAsync(aggRoot, cancellationToken);                

        return result;
    }    
}

