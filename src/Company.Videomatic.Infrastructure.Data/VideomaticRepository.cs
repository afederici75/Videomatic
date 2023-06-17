using Company.Videomatic.Domain.Abstractions;

namespace Company.Videomatic.Infrastructure.Data;

public class VideomaticRepository<TAGGREGATEROOT> : IRepository<TAGGREGATEROOT>
    where TAGGREGATEROOT : IAggregateRoot
{
    private readonly VideomaticDbContext _dbContext;

    public VideomaticRepository(VideomaticDbContext dbContext) 
    {
        _dbContext = dbContext;             
    }

    //public async Task<T?> GetByIdAsync(int id, IEnumerable<string>? includes = null, CancellationToken cancellationToken = default)
    //{
    //    throw new NotImplementedException();
    //    //var qry = new GetOneSpecification<T>(id, includes?.ToArray());
    //    //var res = await  base.FirstOrDefaultAsync(qry, cancellationToken);            
    //    //return res;
    //}

    //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //{
    //    throw new NotImplementedException();
    //    //var res = await base.SaveChangesAsync(cancellationToken);
    //    //_dbContext.ChangeTracker.Clear(); // IMPORTANT!
    //    //return res;
    //}    
}
