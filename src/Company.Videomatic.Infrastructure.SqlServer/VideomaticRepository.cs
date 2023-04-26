using Ardalis.Specification.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Videomatic.Infrastructure.SqlServer;

public class VideomaticRepository<T> : RepositoryBase<T>
    where T : class
{
    private readonly DbContext _dbContext;

    public VideomaticRepository(DbContext dbContext) 
        : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
