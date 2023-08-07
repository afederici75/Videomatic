using Company.SharedKernel.Abstractions;

namespace Company.SharedKernel.Common.CQRS;

public abstract class UpsertAggregateRootHandler<TUpsertCommand, TAggregateRoot, TId> :
    IRequestHandler<TUpsertCommand, Result<TAggregateRoot>>
    where TUpsertCommand : UpsertAggregateRootCommand<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot
    where TId : class
{
    protected UpsertAggregateRootHandler(IRepository<TAggregateRoot> repository, IMapper mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    protected IRepository<TAggregateRoot> Repository { get; }
    protected IMapper Mapper { get; }
    abstract protected TId ConvertIdOfRequest(TUpsertCommand request);

    public Task<Result<TAggregateRoot>> Handle(TUpsertCommand request, CancellationToken cancellationToken)
    {
        if (request.Id.HasValue)
        {
            return UpdateAggregateRoot(request, cancellationToken);
        }
        else
        {
            return InsertAggregateRoot(request, cancellationToken);
        }               
                
    } 

    async Task<Result<TAggregateRoot>> InsertAggregateRoot(TUpsertCommand request, CancellationToken cancellationToken)
    {
        var aggRoot = Mapper.Map<TUpsertCommand, TAggregateRoot>(request);

        var result = await Repository.AddAsync(aggRoot, cancellationToken);

        return result;
    }

    async Task<Result<TAggregateRoot>> UpdateAggregateRoot(TUpsertCommand request, CancellationToken cancellationToken)
    {
        TId id = ConvertIdOfRequest(request);

        TAggregateRoot? currentAgg = await Repository.GetByIdAsync(id, cancellationToken);
        if (currentAgg == null)
        {
            return Result.NotFound();
        }

        // TODO?: this is where I could compare a version-id for the entity...

        // Maps using Automapper which will access private setters to update currentAgg.
        var res = Mapper.Map(request, currentAgg);

        await Repository.UpdateAsync(currentAgg, cancellationToken);

        return res;
    }
}