using Company.SharedKernel.Abstractions;
using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Application.Tests.Features.Videos;
using Company.Videomatic.Domain.Model;
using Company.Videomatic.Infrastructure.SqlServer.Tests;
using Company.Videomatic.Infrastructure.TestData;
using MediatR;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

namespace Company.Videomatic.Integration.Tests;

[Collection("Sequence")]
public class ApplicationTestsForIntegration : ApplicationTests, IClassFixture<VideomaticDbContextFixture>
{
    public ApplicationTestsForIntegration(ITestOutputHelper output, VideomaticDbContextFixture videomaticDbContextFixture)
        : base(output)
    {
        Fixture = videomaticDbContextFixture ?? throw new ArgumentNullException(nameof(videomaticDbContextFixture));
        Fixture.SkipDeletingDatabase();
    }

    public VideomaticDbContextFixture Fixture { get; }

    [Theory()]
    [InlineData(null, null)]
    public override Task DeleteVideoCommandWorksForAllVideos([FromServices] ISender sender, [FromServices] IRepository<Video> repository)
    {
        return base.DeleteVideoCommandWorksForAllVideos(sender, repository);
    }

    [Theory()]
    [InlineData(null, null, null, YouTubeVideos.HyonGakSunim_WhatIsZen)]

    public override Task ImportVideoCommandWorks([FromServices] ISender sender, [FromServices] IRepository<Video> repository, [FromServices] IRepository<Video> repository2, string videoId)
    {
        return base.ImportVideoCommandWorks(sender, repository, repository2, videoId);
    }

    [Theory()]
    [InlineData(null, null)]

    public override Task ImportVideoCommandWorksForAllVideos([FromServices] ISender sender, [FromServices] IRepository<Video> repository)
    {
        return base.ImportVideoCommandWorksForAllVideos(sender, repository);
    }
}
