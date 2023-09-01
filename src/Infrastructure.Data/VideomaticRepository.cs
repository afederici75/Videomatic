using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;

namespace SharedKernel.EntityFrameworkCore;

public class VideomaticRepository<T> : RepositoryBase<T> , IRepository<T>, IReadRepository<T>
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
