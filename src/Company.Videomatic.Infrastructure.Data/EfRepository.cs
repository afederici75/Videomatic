using Ardalis.Specification.EntityFrameworkCore;
using MediatR;
using System.Threading;

namespace Company.Videomatic.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(VideomaticDbContext dbContext) : base(dbContext)
    {
        
    }    
}
