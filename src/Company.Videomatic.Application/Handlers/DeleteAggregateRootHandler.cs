using Microsoft.Extensions.DependencyInjection;

namespace Company.Videomatic.Application.Handlers;

public class DeleteAggregateRootHandler<TDeleteCommand, TAggregateRoot> : IRequestHandler<TDeleteCommand, Result<long>>
    where TDeleteCommand : IDeleteCommand<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot
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

    public async Task<Result<long>> Handle(TDeleteCommand request, CancellationToken cancellationToken)
    {
        var aggRoot = Mapper.Map<TAggregateRoot>(request);
        await Repository.DeleteAsync(aggRoot);

        await Repository.SaveChangesAsync();

        var itemToDelete = await Repository.GetByIdAsync(request.Id, cancellationToken);
        //
        //if (itemToDelete == null)
        //    return Result.NotFound();
        //
        //// Deletes the entity
        //await Repository.DeleteAsync(itemToDelete);

        return request.Id;
    }
}