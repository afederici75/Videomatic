using SharedKernel.Abstractions;

namespace SharedKernel.CQRS.Commands;

public abstract class UpsertEntityHandler<TUpsertCommand, TEntity, TId> :
    IRequestHandler<TUpsertCommand, Result<TEntity>>
    where TUpsertCommand : IRequest<Result<TEntity>>
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
    
    public async Task<Result<TEntity>> Handle(TUpsertCommand request, CancellationToken cancellationToken)
    {
        try
        {
            throw new NotImplementedException();
            //if (request.Id.HasValue)
            //{
            //    TId id = (TId)Activator.CreateInstance(typeof(TId), request.Id)!;
            //
            //    var stored = await Repository.GetByIdAsync(id, cancellationToken);
            //    if (stored == null)
            //    {
            //        return Result.NotFound();
            //    }
            //
            //    var final = Mapper.Map(request, stored);
            //    await Repository.UpdateAsync(final, cancellationToken);
            //
            //    return Result.Success(stored);
            //}
            //else
            //{
            //    var entity = Mapper.Map<TUpsertCommand, TEntity>(request);
            //
            //    var result = await Repository.AddAsync(entity, cancellationToken);
            //
            //    return result;
            //}
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }   
}