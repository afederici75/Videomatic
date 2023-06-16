using Company.Videomatic.Infrastructure.Data;
using Xunit.Abstractions;

namespace Company.Videomatic.Application.Tests;

public abstract class RepositoryTestsBase<TEntity> : IClassFixture<RepositoryFixture<TEntity>>
    where TEntity : class, /*IEntity, */IAggregateRoot  
{
    public RepositoryTestsBase(RepositoryFixture<TEntity> fixture)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
    }

    public RepositoryFixture<TEntity> Fixture { get; }
}

