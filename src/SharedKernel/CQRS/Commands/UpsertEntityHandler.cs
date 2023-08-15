using SharedKernel.Abstractions;

namespace SharedKernel.CQRS.Commands;

public abstract class UpsertEntityHandler<TUpsertCommand, TEntity, TId> :
    IRequestHandler<TUpsertCommand, Result<TEntity>>
    where TUpsertCommand : UpsertEntityCommand<TEntity>
    where TEntity : class
    where TId : class
{
    protected UpsertEntityHandler(IRepository<TEntity> repository, IMapper mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    protected IRepository<TEntity> Repository { get; }
    protected IMapper Mapper { get; }
    abstract protected TId ConvertIdOfRequest(TUpsertCommand request);

    public Task<Result<TEntity>> Handle(TUpsertCommand request, CancellationToken cancellationToken)
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

    async Task<Result<TEntity>> InsertAggregateRoot(TUpsertCommand request, CancellationToken cancellationToken)
    {
        var aggRoot = Mapper.Map<TUpsertCommand, TEntity>(request);

        var result = await Repository.AddAsync(aggRoot, cancellationToken);

        return result;
    }

    async Task<Result<TEntity>> UpdateAggregateRoot(TUpsertCommand request, CancellationToken cancellationToken)
    {
        TId id = ConvertIdOfRequest(request);

        TEntity? currentAgg = await Repository.GetByIdAsync(id, cancellationToken);
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