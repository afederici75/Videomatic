﻿using Ardalis.Specification.EntityFrameworkCore;
using MediatR;
using System.Threading;

namespace Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class
{
    //public EfRepository(VideomaticDbContext dbContext) : base(dbContext)
    //{
    //    
    //}

    public EfRepository(IDbContextFactory<VideomaticDbContext> factory) 
        : base(factory.CreateDbContext())
    {
        
    }
}
