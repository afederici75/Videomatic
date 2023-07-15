using AutoMapper;
using Company.SharedKernel.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Company.SharedKernel.Handlers;

public abstract class CreateAggregateRootHandler<TCreateCommand, TAggregateRoot> : 
    IRequestHandler<TCreateCommand, Result<TAggregateRoot>>
    where TCreateCommand : IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class, IAggregateRoot
{
    protected CreateAggregateRootHandler(IServiceProvider serviceProvider, IMapper mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        Repository = (IRepository<TAggregateRoot>)ServiceProvider.GetRequiredService(typeof(IRepository<TAggregateRoot>));
    }

    protected IServiceProvider ServiceProvider { get; }
    protected IRepository<TAggregateRoot> Repository { get; }
    protected IMapper Mapper { get; }

    public async Task<Result<TAggregateRoot>> Handle(TCreateCommand request, CancellationToken cancellationToken)
    {
        var aggRoot = Mapper.Map<TCreateCommand, TAggregateRoot>(request);
        
        var result = await Repository.AddAsync(aggRoot, cancellationToken);                

        return result;
    }    
}
