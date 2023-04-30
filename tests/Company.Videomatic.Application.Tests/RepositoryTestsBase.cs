using Company.Videomatic.Infrastructure.Data;
using Xunit.Abstractions;

namespace Company.Videomatic.Application.Tests;

public abstract class RepositoryTestsBase<TDbContext, TEntity> : IClassFixture<RepositoryFixture<TDbContext, TEntity>>
    where TEntity : class
    where TDbContext : VideomaticDbContext
{
    public RepositoryTestsBase(RepositoryFixture<TDbContext, TEntity> fixture)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
    }

    public RepositoryFixture<TDbContext, TEntity> Fixture { get; }
}

