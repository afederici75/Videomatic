using Microsoft.Extensions.DependencyInjection;

namespace Company.Videomatic.Application.Handlers;

public abstract class DeleteAggregateRootHandler<TDeleteCommand, TAggregateRoot, TId> : IRequestHandler<TDeleteCommand, Result<bool>>
    where TDeleteCommand : IDeleteCommand<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot<TId>
    where TId : class
{
    public DeleteAggregateRootHandler(IServiceProvider serviceProvider, IMapper mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        var repoType = typeof(IRepository<TAggregateRoot>);
        Repository = (IRepository<TAggregateRoot>)ServiceProvider.GetRequiredService(repoType);
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

