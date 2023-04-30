using Company.Videomatic.Infrastructure.Data;
using Xunit.Abstractions;

namespace Company.Videomatic.Application.Tests;

public partial class VideosTestsBase<TDbContext>
    where TDbContext : VideomaticDbContext
{
    public ITestOutputHelper Output { get; }

    public VideosTestsBase(ITestOutputHelper testOutputHelper)
    {
        Output = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
    }

    
}

