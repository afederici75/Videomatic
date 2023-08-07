using Company.SharedKernel.Abstractions;

namespace Company.SharedKernel.CQRS.Commands;

public abstract class CreateEntitytHandler<TCreateCommand, TEntity> :
    IRequestHandler<TCreateCommand, Result<TEntity>>
    where TCreateCommand : IRequest<Result<TEntity>>
    where TEntity : class, IEntity
{
    protected CreateEntitytHandler(IRepository<TEntity> repository, IMapper mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    protected IRepository<TEntity> Repository { get; }
    protected IMapper Mapper { get; }

    public async Task<Result<TEntity>> Handle(TCreateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggRoot = Mapper.Map<TCreateCommand, TEntity>(request);

            var result = await Repository.AddAsync(aggRoot, cancellationToken);

            return result;
        }
        catch (Exception ex)
        {
            return Result<TEntity>.Error(ex.Message);
        }
    }
}
