namespace Company.Videomatic.Application.Handlers;

public abstract class DeleteAggregateRootHandler<TDeleteCommand, TAggregateRoot, TId> : 
    AggregateRootCommandHandlerBase<TDeleteCommand, TAggregateRoot>,
    IRequestHandler<TDeleteCommand, Result<bool>>
    where TDeleteCommand : IDeleteCommand<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot<TId>
    where TId : class
{
    public DeleteAggregateRootHandler(IServiceProvider serviceProvider, IMapper mapper) 
        : base(serviceProvider, mapper)
    {
    }

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

