using Company.Videomatic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Company.Videomatic.Application.Tests;

public class RepositoryFixture<TDbContext, T>
    where T : class
    where TDbContext : VideomaticDbContext
{
    public RepositoryFixture(TDbContext dbContext, IRepositoryBase<T> repository, ITestOutputHelperAccessor outputAccessor)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _outputAccessor = outputAccessor ?? throw new ArgumentNullException(nameof(outputAccessor));

        DbContext.Database.EnsureDeleted();
        DbContext.Database.Migrate();// EnsureCreated();
    }

    public TDbContext DbContext { get; }
    public IRepositoryBase<T> Repository { get; }

    readonly ITestOutputHelperAccessor _outputAccessor;

    public ITestOutputHelper Output => _outputAccessor.Output ?? throw new Exception("XXX");
}

