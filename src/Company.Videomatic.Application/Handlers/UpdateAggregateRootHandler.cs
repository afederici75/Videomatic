using Microsoft.Extensions.DependencyInjection;

namespace Company.Videomatic.Application.Handlers;

public class UpdateAggregateRootHandler<TUpdateCommand, TAggregateRoot> : IRequestHandler<TUpdateCommand, Result<TAggregateRoot>>
    where TUpdateCommand : IUpdateCommand<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot
{
    public UpdateAggregateRootHandler(IServiceProvider serviceProvider, IMapper mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        var repoType = typeof(IRepository<TAggregateRoot>);
        Repository = (IRepository<TAggregateRoot>)ServiceProvider.GetRequiredService(repoType);
    }

    protected IServiceProvider ServiceProvider { get; }
    protected IRepository<TAggregateRoot> Repository { get; }
    protected IMapper Mapper { get; }

    public async Task<Result<TAggregateRoot>> Handle(TUpdateCommand request, CancellationToken cancellationToken)
    {
        var id = Mapper.Map<TAggregateRoot>(request)
                       .GetId();

        var itemToUpdate = await Repository.GetByIdAsync(id, cancellationToken);
        if (itemToUpdate == null)
        {
            return Result.NotFound();
        }

        Mapper.Map(request, itemToUpdate);
        await Repository.UpdateAsync(itemToUpdate);

        return itemToUpdate;
    }
}
