using Microsoft.Extensions.DependencyInjection;

namespace Company.SharedKernel.Abstractions;

public abstract class UpsertAggregateRootHandler<TUpsertCommand, TAggregateRoot, TId> :
    IRequestHandler<TUpsertCommand, Result<TAggregateRoot>>
    where TUpsertCommand : UpsertAggregateRootCommand<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot
    where TId : class
{
    protected UpsertAggregateRootHandler(IServiceProvider serviceProvider, IMapper mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        var repoType = typeof(IRepository<TAggregateRoot>);
        Repository = (IRepository<TAggregateRoot>)ServiceProvider.GetRequiredService(repoType);
    }

    protected IServiceProvider ServiceProvider { get; }
    protected IRepository<TAggregateRoot> Repository { get; }
    protected IMapper Mapper { get; }
    abstract protected TId ConvertIdOfRequest(TUpsertCommand request);

    public Task<Result<TAggregateRoot>> Handle(TUpsertCommand request, CancellationToken cancellationToken)
        => !request.Id.HasValue ? InsertAggregateRoot(request, cancellationToken): UpdateAggregateRoot(request, cancellationToken);

    public async Task<Result<TAggregateRoot>> InsertAggregateRoot(TUpsertCommand request, CancellationToken cancellationToken)
    {
        var aggRoot = Mapper.Map<TUpsertCommand, TAggregateRoot>(request);

        var result = await Repository.AddAsync(aggRoot, cancellationToken);

        return result;
    }

    private async Task<Result<TAggregateRoot>> UpdateAggregateRoot(TUpsertCommand request, CancellationToken cancellationToken)
    {
        TId id = ConvertIdOfRequest(request);

        TAggregateRoot? currentAgg = await Repository.GetByIdAsync(id, cancellationToken);
        if (currentAgg == null)
        {
            return Result.NotFound();
        }

        // TODO?: this is where I could compare a version-id for the entity...

        // Maps using Automapper which will access private setters to update currentAgg.
        var res = Mapper.Map<TUpsertCommand, TAggregateRoot>(request, currentAgg);

        await Repository.UpdateAsync(currentAgg, cancellationToken);

        return res;
    }
}