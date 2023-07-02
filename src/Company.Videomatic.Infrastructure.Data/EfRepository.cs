using Ardalis.Specification.EntityFrameworkCore;

namespace Company.Videomatic.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(VideomaticDbContext dbContext) : base(dbContext)
    {
    }
}
