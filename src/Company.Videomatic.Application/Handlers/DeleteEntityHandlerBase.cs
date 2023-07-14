//namespace Company.Videomatic.Application.Handlers;

//public abstract class DeleteEntityHandlerBase<TRequest, TResponse, TEntity, TId> : IRequestHandler<TRequest, TResponse>
//    where TRequest : IRequest<TResponse>, ICommandWithEntityId
//    where TEntity : class, IAggregateRoot
//    where TId : class
//{
//    public DeleteEntityHandlerBase(IRepository<TEntity> repository, IMapper mapper)
//    {
//        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
//        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
//    }

//    protected IRepository<TEntity> Repository { get; }
//    protected IMapper Mapper { get; }

//    protected abstract TId GetIdOfRequest(TRequest request);
//    protected abstract TResponse CreateResponseFor(TId entityId, bool wasDeleted);

//    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
//    {
//        // Tries to find the entity by the Id in the request
//        TId entityId = GetIdOfRequest(request);

//        var itemToDelete = await Repository.GetByIdAsync(entityId, cancellationToken);
        
//        if (itemToDelete == null)
//            return CreateResponseFor(entityId, false);

//        // Deletes the entity
//        await Repository.DeleteAsync(itemToDelete);

//        return CreateResponseFor(entityId, true);
//    }
//}
