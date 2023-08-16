using SharedKernel.Abstractions;

namespace SharedKernel.CQRS.Commands;

public abstract class DeleteEntityHandler<TDeleteCommand, TEntity, TId> : IRequestHandler<TDeleteCommand, Result>
    where TDeleteCommand : IRequest<Result>, IRequestWithId
    where TEntity : class
    where TId : struct
{
    public DeleteEntityHandler(IRepository<TEntity> repository, IMapper mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    protected IRepository<TEntity> Repository { get; }
    protected IMapper Mapper { get; }

    public async Task<Result> Handle(TDeleteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            TId id = (TId)Activator.CreateInstance(typeof(TId), request.Id)!;
            
            var itemToDelete = await Repository.GetByIdAsync(id, cancellationToken);
            if (itemToDelete == null)
            {
                return Result.NotFound();
            }

            // TODO: this is where I could compare a version-id for the entity...
            await Repository.DeleteAsync(itemToDelete, cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }    
}

