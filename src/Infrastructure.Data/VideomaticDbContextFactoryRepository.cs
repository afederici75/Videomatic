using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;

namespace SharedKernel.EntityFrameworkCore;

//public class VideomaticDbContextFactoryRepository<TEntity, TDbContext> : ContextFactoryRepositoryBaseOfT<TEntity, TDbContext>,
//        IMyRepository<TEntity>
//        where TEntity : class
//        where TDbContext : DbContext
//{
//    public VideomaticDbContextFactoryRepository(IDbContextFactory<TDbContext> dbContextFactory) : base(dbContextFactory)
//    {
//    }

//    public VideomaticDbContextFactoryRepository(IDbContextFactory<TDbContext> dbContextFactory, ISpecificationEvaluator specificationEvaluator) : base(dbContextFactory, specificationEvaluator)
//    {
//    }
//}

public class VideomaticRepositoryFactory<TEntity> : EFRepositoryFactory<IRepository<TEntity>, VideomaticRepository<TEntity>, VideomaticDbContext>
    where TEntity : class
{
    public VideomaticRepositoryFactory(IDbContextFactory<VideomaticDbContext> dbContextFactory) : base(dbContextFactory)
    {
    }
}

public class VideomaticRepository<T> : RepositoryBase<T> , IRepository<T>
    where T : class
{
    protected readonly VideomaticDbContext dbContext;

    public VideomaticRepository(VideomaticDbContext dbContext) : this(dbContext, SpecificationEvaluator.Default)
    {
    }

    public VideomaticRepository(VideomaticDbContext dbContext, ISpecificationEvaluator specificationEvaluator) : base(dbContext, specificationEvaluator)
    {
        this.dbContext = dbContext;
    }
}
