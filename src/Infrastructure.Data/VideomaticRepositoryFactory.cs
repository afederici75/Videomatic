using Ardalis.Specification.EntityFrameworkCore;
using Infrastructure.Data;

namespace SharedKernel.EntityFrameworkCore;

public class VideomaticRepositoryFactory<TEntity> : EFRepositoryFactory<IRepository<TEntity>, VideomaticRepository<TEntity>, VideomaticDbContext>
    where TEntity : class
{
    public VideomaticRepositoryFactory(IDbContextFactory<VideomaticDbContext> dbContextFactory) : base(dbContextFactory)
    {
    }
}
