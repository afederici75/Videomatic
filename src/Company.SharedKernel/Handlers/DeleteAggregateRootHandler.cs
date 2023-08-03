using Company.SharedKernel.Abstractions;

namespace Company.SharedKernel.Handlers;

public abstract class DeleteAggregateRootHandler<TDeleteCommand, TAggregateRoot, TId> : IRequestHandler<TDeleteCommand, Result<bool>>
    where TDeleteCommand : IRequest<Result<bool>>
    where TAggregateRoot : class, IAggregateRoot
    where TId : class
{    
    public DeleteAggregateRootHandler(IRepository<TAggregateRoot> repository, IMapper mapper) 
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    protected IRepository<TAggregateRoot> Repository { get; }
    protected IMapper Mapper { get; }

    public async Task<Result<bool>> Handle(TDeleteCommand request, CancellationToken cancellationToken)
    {
        object id = ConvertIdOfRequest(request);

        var itemToDelete = await Repository.GetByIdAsync(id, cancellationToken);
        if (itemToDelete == null)
        {
            return Result.NotFound();
        }

        // TODO: this is where I could compare a version-id for the entity...

        await Repository.DeleteAsync(itemToDelete);
        return true;
    }

    protected abstract TId ConvertIdOfRequest(TDeleteCommand request);
}

