using Xunit.Abstractions;

namespace Company.Videomatic.Application.Tests;

public class RepositoryFixture<T>
    where T : class
{
    public RepositoryFixture(IRepositoryBase<T> repository, ITestOutputHelperAccessor outputAccessor)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _outputAccessor = outputAccessor ?? throw new ArgumentNullException(nameof(outputAccessor));
    }

    public IRepositoryBase<T> Repository { get; }

    readonly ITestOutputHelperAccessor _outputAccessor;

    public ITestOutputHelper Output => _outputAccessor.Output ?? throw new Exception("XXX");
}

