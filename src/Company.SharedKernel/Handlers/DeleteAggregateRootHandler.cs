using Company.SharedKernel.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Company.SharedKernel.Handlers;

public abstract class DeleteAggregateRootHandler<TDeleteCommand, TAggregateRoot, TId> : IRequestHandler<TDeleteCommand, Result<bool>>
    where TDeleteCommand : IRequest<Result<bool>>
    where TAggregateRoot : class, IAggregateRoot
    where TId : class
{    
    public DeleteAggregateRootHandler(IServiceProvider serviceProvider, IMapper mapper) 
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        Repository = (IRepository<TAggregateRoot>)ServiceProvider.GetRequiredService(typeof(IRepository<TAggregateRoot>));
    }

    protected IServiceProvider ServiceProvider { get; }
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

