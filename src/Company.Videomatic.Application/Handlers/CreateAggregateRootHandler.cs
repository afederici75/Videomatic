using Company.Videomatic.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Videomatic.Application.Handlers;

public abstract class CreateAggregateRootHandler<TCreateCommand, TAggregateRoot> : 
    AggregateRootCommandHandlerBase<TCreateCommand, TAggregateRoot>,
    IRequestHandler<TCreateCommand, Result<TAggregateRoot>>
    where TCreateCommand : ICreateCommand<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot
{
    protected CreateAggregateRootHandler(IServiceProvider serviceProvider, IMapper mapper) 
        : base(serviceProvider, mapper)
    {
    }

    public async Task<Result<TAggregateRoot>> Handle(TCreateCommand request, CancellationToken cancellationToken)
    {
        var aggRoot = Mapper.Map<TCreateCommand, TAggregateRoot>(request);
        
        var result = await Repository.AddAsync(aggRoot, cancellationToken);                

        return result;
    }    
}

