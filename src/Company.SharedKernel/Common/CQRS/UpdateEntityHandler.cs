using Company.SharedKernel.Abstractions;

namespace Company.SharedKernel.Common.CQRS;

public abstract class UpdateEntityHandler<TUpdateCommand, TEntity, TId> :
    IRequestHandler<TUpdateCommand, Result<TEntity>>
    where TUpdateCommand : UpdateEntityCommand<TEntity>, IRequestWithId
    where TEntity : class, IEntity
    where TId : class
{
    protected UpdateEntityHandler(IRepository<TEntity> repository, IMapper mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    protected IRepository<TEntity> Repository { get; }
    protected IMapper Mapper { get; }
    
    public async Task<Result<TEntity>> Handle(TUpdateCommand request, CancellationToken cancellationToken)
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

    protected TId ConvertIdOfRequest(IRequestWithId request)
    {
        TId result = (TId)Activator.CreateInstance(typeof(TId), request.Id)!;

        return result;
    }
}