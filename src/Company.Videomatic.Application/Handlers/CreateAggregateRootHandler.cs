using Microsoft.Extensions.DependencyInjection;

namespace Company.Videomatic.Application.Handlers;

public class CreateAggregateRootHandler<TCreateCommand, TAggregateRoot> : IRequestHandler<TCreateCommand, Result<long>>
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

    public async Task<Result<long>> Handle(TCreateCommand request, CancellationToken cancellationToken)
    {
        var aggRoot = Mapper.Map<ICreateCommand<TAggregateRoot>, TAggregateRoot>(request);
        var result = await Repository.AddAsync(aggRoot, cancellationToken);        
        //await Repository.SaveChangesAsync( cancellationToken);
        return result.GetId();
    }
}

