using Xunit.Abstractions;

namespace Company.Videomatic.Application.Tests;

[Collection("Sequence")]
public partial class VideosTests
{
    public VideosTests(VideomaticRepositoryFixture fixture, ITestOutputHelper output)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        Output = output ?? throw new ArgumentNullException(nameof(output));
    }

    protected VideomaticRepositoryFixture Fixture { get; }
    protected ITestOutputHelper Output { get; }
}
