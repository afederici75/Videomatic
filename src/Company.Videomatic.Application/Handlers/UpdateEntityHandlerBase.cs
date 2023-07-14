//namespace Company.Videomatic.Application.Handlers;

//public abstract class UpdateEntityHandlerBase<TRequest, TResponse, TEntity, TId> : IRequestHandler<TRequest, TResponse>
//    where TRequest : IRequest<TResponse>
//    where TEntity : class, IAggregateRoot
//    where TId: class
//{
//    public UpdateEntityHandlerBase(IRepository<TEntity> repository, IMapper mapper)
//    {
//        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
//        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
//    }
//
//    protected IRepository<TEntity> Repository { get; }
//    protected IMapper Mapper { get; }
//
//    protected abstract TId GetIdOfRequest(TRequest request);
//    protected abstract TResponse CreateResponseFor(TId updatedEntityId, bool wasUpdated);
//
//    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
//    {
//        // Tries to find the entity by the Id in the request
//        TId entityId = GetIdOfRequest(request);
//        var artifact = await Repository.GetByIdAsync(entityId, cancellationToken);
//        if (artifact == null)
//            return CreateResponseFor(entityId, false);
//
//        // Updates
//        Mapper.Map(request, artifact);
//        await Repository.UpdateAsync(artifact);
//
//        return CreateResponseFor(entityId, true);
//    }
//}