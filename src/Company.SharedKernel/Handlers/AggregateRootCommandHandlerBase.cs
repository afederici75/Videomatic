using Company.SharedKernel.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Company.SharedKernel.Handlers;

public abstract class AggregateRootCommandHandlerBase<TCommand, TAggregateRoot>
    where TCommand : IAggregateRootCommand<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot    
{ 
    public AggregateRootCommandHandlerBase(IServiceProvider serviceProvider, IMapper mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        var repoType = typeof(IRepository<TAggregateRoot>);
        Repository = (IRepository<TAggregateRoot>)ServiceProvider.GetRequiredService(repoType);
    }

    protected IServiceProvider ServiceProvider { get; }
    protected IRepository<TAggregateRoot> Repository { get; }
    protected IMapper Mapper { get; }
}

