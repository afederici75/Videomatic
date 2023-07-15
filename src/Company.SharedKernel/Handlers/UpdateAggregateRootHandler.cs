using Company.SharedKernel.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Company.SharedKernel.Handlers;

public abstract class UpdateAggregateRootHandler<TUpdateCommand, TAggregateRoot, TId> :
    IRequestHandler<TUpdateCommand, Result<TAggregateRoot>>
    where TUpdateCommand : UpdateAggregateRootCommand<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot
    where TId: class
{
    protected UpdateAggregateRootHandler(IServiceProvider serviceProvider, IMapper mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        var repoType = typeof(IRepository<TAggregateRoot>);
        Repository = (IRepository<TAggregateRoot>)ServiceProvider.GetRequiredService(repoType);
    }

    protected IServiceProvider ServiceProvider { get; }
    protected IRepository<TAggregateRoot> Repository { get; }
    protected IMapper Mapper { get; }
    abstract protected TId ConvertIdOfRequest(TUpdateCommand request);

    public async Task<Result<TAggregateRoot>> Handle(TUpdateCommand request, CancellationToken cancellationToken)
    {
        TId id = ConvertIdOfRequest(request);
        
        TAggregateRoot? currentAgg = await Repository.GetByIdAsync(id, cancellationToken);
        if (currentAgg == null)
        {
            return Result.NotFound();
        }

        // TODO?: this is where I could compare a version-id for the entity...

        // Maps using Automapper which will access private setters to update currentAgg.
        var res = Mapper.Map<TUpdateCommand, TAggregateRoot>(request, currentAgg);

        await Repository.UpdateAsync(currentAgg, cancellationToken);
        
        return res;
    }
}