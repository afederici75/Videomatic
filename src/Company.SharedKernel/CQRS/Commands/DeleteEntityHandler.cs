using Company.SharedKernel.Abstractions;
using System;

namespace Company.SharedKernel.CQRS.Commands;

public abstract class DeleteEntityHandler<TDeleteCommand, TAggregateRoot, TId> : IRequestHandler<TDeleteCommand, Result<bool>>
    where TDeleteCommand : IRequest<Result<bool>>, IRequestWithId
    where TAggregateRoot : class, IEntity
    where TId : class
{
    public DeleteEntityHandler(IRepository<TAggregateRoot> repository, IMapper mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    protected IRepository<TAggregateRoot> Repository { get; }
    protected IMapper Mapper { get; }

    public async Task<Result<bool>> Handle(TDeleteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            object id = ConvertIdOfRequest(request);

            var itemToDelete = await Repository.GetByIdAsync(id, cancellationToken);
            if (itemToDelete == null)
            {
                return Result.NotFound();
            }

            // TODO: this is where I could compare a version-id for the entity...

            await Repository.DeleteAsync(itemToDelete, cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    /// <summary>
    /// This method converts the id contained in the request to the type of the id of the aggregate root.    
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected TId ConvertIdOfRequest(IRequestWithId request)
    {
        TId result = (TId)Activator.CreateInstance(typeof(TId), request.Id)!;

        return result;
    }
}

