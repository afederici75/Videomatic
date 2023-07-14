//namespace Company.Videomatic.Application.Handlers;

//public abstract class CreateEntityHandlerBase<TRequest, TResponse, TEntity> : IRequestHandler<TRequest, TResponse>
//    where TRequest : IRequest<TResponse>
//    where TEntity : class, IAggregateRoot
//{    
//    public CreateEntityHandlerBase(IRepository<TEntity> repository, IMapper mapper)
//    {
//        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
//        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
//    }

//    protected IRepository<TEntity> Repository { get; }
//    protected IMapper Mapper { get; }

//    protected abstract TResponse CreateResponseFor(TEntity createdEntity);

//    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
//    {
//        TEntity newItem = Mapper.Map<TRequest, TEntity>(request);
        
//        TEntity entry = await Repository.AddAsync(newItem);

//        return CreateResponseFor(entry);
//    }
//}

////public abstract class CreateEntityHandlerBase2<TRequest, TResponse, TEntity> : IRequestHandler<TRequest, TResponse>
////    where TRequest : IRequest<TResponse>, ICommandWithEntityId
////    where TResponse : class, IResult
////    where TEntity : class, IAggregateRoot
////{
////    public CreateEntityHandlerBase2(IRepository<TEntity> repository, IMapper mapper)
////    {
////        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
////        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
////    }
////
////    protected IRepository<TEntity> Repository { get; }
////    protected IMapper Mapper { get; }
////
////    protected abstract TResponse CreateResponseFor(TEntity createdEntity);
////
////    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
////    {
////        TEntity newItem = Mapper.Map<TRequest, TEntity>(request);
////
////        try
////        {
////            TEntity entry = await Repository.AddAsync(newItem);
////            
////            var response = CreateResponseFor(entry);
////            return response;
////        }
////        catch (Exception ex)
////        {
////            return Result<TResponse>.Error(ex.Message);
////        }
////    }
////}