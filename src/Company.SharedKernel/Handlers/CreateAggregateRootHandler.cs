using Company.SharedKernel.Abstractions;

namespace Company.SharedKernel.Handlers;

public abstract class CreateAggregateRootHandler<TCreateCommand, TAggregateRoot> : 
    IRequestHandler<TCreateCommand, Result<TAggregateRoot>>
    where TCreateCommand : IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class, IAggregateRoot
{
    protected CreateAggregateRootHandler(IRepository<TAggregateRoot> repository, IMapper mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    protected IRepository<TAggregateRoot> Repository { get; }
    protected IMapper Mapper { get; }

    public async Task<Result<TAggregateRoot>> Handle(TCreateCommand request, CancellationToken cancellationToken)
    {
        var aggRoot = Mapper.Map<TCreateCommand, TAggregateRoot>(request);
        
        var result = await Repository.AddAsync(aggRoot, cancellationToken);                

        return result;
    }    
}
