using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Application.Tests.Features.Videos;
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
        Fixture.SkipDeletingDatabase();
    }

    public VideomaticDbContextFixture Fixture { get; }
    
}
