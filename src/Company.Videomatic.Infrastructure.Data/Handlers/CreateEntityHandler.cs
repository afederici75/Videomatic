namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public abstract class CreateEntityHandler<TRequest, TResponse, TEntity> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TEntity : class, IAggregateRoot
{    
    public CreateEntityHandler(IRepository<TEntity> repository, IMapper mapper)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    protected IRepository<TEntity> Repository { get; }
    protected IMapper Mapper { get; }

    protected abstract TResponse CreateResponseFromEntity(TEntity entity);

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        TEntity newItem = Mapper.Map<TRequest, TEntity>(request);

        TEntity entry = await Repository.AddAsync(newItem);

        return CreateResponseFromEntity(entry);
    }
}