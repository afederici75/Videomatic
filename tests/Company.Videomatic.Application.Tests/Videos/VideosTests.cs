using Xunit.Abstractions;

namespace Company.Videomatic.Application.Tests.Videos;

[Collection("Sequence")]
public partial class VideosTests : IClassFixture<VideomaticDbContextFixture>
{
    public VideosTests(VideomaticDbContextFixture fixture, ITestOutputHelper output)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        Output = output ?? throw new ArgumentNullException(nameof(output));
    }

    protected VideomaticDbContextFixture Fixture { get; }
    protected ITestOutputHelper Output { get; }
}
