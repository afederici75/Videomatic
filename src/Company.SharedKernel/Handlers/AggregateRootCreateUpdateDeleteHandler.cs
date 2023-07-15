using Company.SharedKernel.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Company.SharedKernel.Handlers;

public abstract class AggregateRootCreateUpdateDeleteHandler<TCreateOrUpdateCommand, TAggregateRoot, TId> :
    IRequestHandler<TCreateOrUpdateCommand, Result<TAggregateRoot>>
    where TCreateOrUpdateCommand : IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class, IAggregateRoot
    where TId : class
{
    protected AggregateRootCreateUpdateDeleteHandler(IServiceProvider serviceProvider, IMapper mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        Repository = (IRepository<TAggregateRoot>)ServiceProvider.GetRequiredService(typeof(IRepository<TAggregateRoot>));
    }

    protected IServiceProvider ServiceProvider { get; }
    protected IRepository<TAggregateRoot> Repository { get; }
    protected IMapper Mapper { get; }

    public async Task<Result<TAggregateRoot>> Handle(TCreateOrUpdateCommand request, CancellationToken cancellationToken)
    {
        var aggRoot = Mapper.Map<TCreateOrUpdateCommand, TAggregateRoot>(request);

        var result = await Repository.AddAsync(aggRoot, cancellationToken);

        return result;
    }
}