namespace Company.Videomatic.Application.Tests;

public class RepositoryFixture<T>
    where T : class
{
    public RepositoryFixture(IRepositoryBase<T> repository) => Repository = repository;

    public IRepositoryBase<T> Repository { get; }
}

