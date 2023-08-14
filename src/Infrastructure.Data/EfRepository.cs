using Ardalis.Specification.EntityFrameworkCore;
using MediatR;
using System.Threading;

namespace Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IEntity
{
    public EfRepository(VideomaticDbContext dbContext) : base(dbContext)
    {
        
    }    
}
