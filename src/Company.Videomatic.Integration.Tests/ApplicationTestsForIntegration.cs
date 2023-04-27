using Company.SharedKernel.Abstractions;
using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Application.Tests.Features.Videos;
using Company.Videomatic.Domain.Model;
using Company.Videomatic.Infrastructure.SqlServer.Tests;
using MediatR;
using Xunit.DependencyInjection;

namespace Company.Videomatic.Integration.Tests;

[Collection("Sequence")]
public class ApplicationTestsForIntegration : ApplicationTests, IClassFixture<VideomaticDbContextFixture>
{
    public ApplicationTestsForIntegration(VideomaticDbContextFixture videomaticDbContextFixture)
    {
        Fixture = videomaticDbContextFixture ?? throw new ArgumentNullException(nameof(videomaticDbContextFixture));
        //Fixture.SkipDeletingDatabase();
    }

    public VideomaticDbContextFixture Fixture { get; }

    public override Task DeleteVideoCommandWorksForAllVides([FromServices] ISender sender, [FromServices] IRepository<Video> repository)
    {
        return base.DeleteVideoCommandWorksForAllVides(sender, repository);
    }

    // It is possible to exclude inherited tests from the test run
    //[Theory(Skip = "test skip")]
    //[InlineData(null, null, null)]
    public override Task ImportVideoCommandWorks([FromServices] ISender sender, [FromServices] IRepository<Video> repository, string videoId)
    {
        return base.ImportVideoCommandWorks(sender, repository, videoId);
    }

    public override Task ImportVideoCommandWorksForAllVides([FromServices] ISender sender, [FromServices] IRepository<Video> repository)
    {
        return base.ImportVideoCommandWorksForAllVides(sender, repository);
    }
}
