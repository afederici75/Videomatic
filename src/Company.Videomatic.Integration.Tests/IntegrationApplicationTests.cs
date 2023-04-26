using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Application.Tests.Features.Videos;
using Company.Videomatic.Infrastructure.SqlServer.Tests;
using MediatR;
using Xunit.DependencyInjection;

namespace Company.Videomatic.Integration.Tests;

[Collection("Sequence")]
public class IntegrationApplicationTests : ApplicationTests, IClassFixture<VideomaticDbContextFixture>
{
    public IntegrationApplicationTests(VideomaticDbContextFixture videomaticDbContextFixture)
    {
        Fixture = videomaticDbContextFixture ?? throw new ArgumentNullException(nameof(videomaticDbContextFixture));
        Fixture.SkipDeletingDatabase();
    }

    public VideomaticDbContextFixture Fixture { get; }

    public override Task ImportVideoUsingISender([FromServices] ISender sender, string videoId)
    {
        return base.ImportVideoUsingISender(sender, videoId);
    }

    public override Task ImportVideoUsingISender2([FromServices] ISender sender, [FromServices] IVideoRepository repository, string videoId)
    {
        return base.ImportVideoUsingISender2(sender, repository, videoId);
    }

    public override Task ImportVideoUsingISender3([FromServices] ISender sender, [FromServices] IVideoRepository repository)
    {
        return base.ImportVideoUsingISender3(sender, repository);
    }
}
