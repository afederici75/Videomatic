using Company.Videomatic.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Videomatic.Application.Handlers;

public abstract class UpdateAggregateRootHandler<TUpdateCommand, TAggregateRoot, TId> :
    AggregateRootCommandHandlerBase<TUpdateCommand, TAggregateRoot>, 
    IRequestHandler<TUpdateCommand, Result<TAggregateRoot>>
    where TUpdateCommand : IUpdateCommand<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot
    where TId: class
{
    protected UpdateAggregateRootHandler(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper) { }

    abstract protected TId ConvertIdOfRequest(TUpdateCommand request);

    public async Task<Result<TAggregateRoot>> Handle(TUpdateCommand request, CancellationToken cancellationToken)
    {
        TId id = ConvertIdOfRequest(request);
        
        TAggregateRoot? currentAgg = await Repository.GetByIdAsync(id, cancellationToken);
        if (currentAgg == null)
        {
            return Result.NotFound();
        }

        // TODO?: this is where I could compare a version-id for the entity...

        // Maps using Automapper which will access private setters to update currentAgg.
        var res = Mapper.Map<TUpdateCommand, TAggregateRoot>(request, currentAgg);

        await Repository.UpdateAsync(currentAgg, cancellationToken);
        
        return res;
    }
}