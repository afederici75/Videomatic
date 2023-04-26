using Company.Videomatic.Application.Tests.Features.Videos;
using Company.Videomatic.Infrastructure.SqlServer.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Videomatic.Integration.Tests;

public class IntegrationApplicationTests : ApplicationTests, IClassFixture<VideomaticDbContextFixture>
{
    public IntegrationApplicationTests(VideomaticDbContextFixture videomaticDbContextFixture)
    {
        Fixture = videomaticDbContextFixture ?? throw new ArgumentNullException(nameof(videomaticDbContextFixture));
        Fixture.SkipDeletingDatabase();
    }

    public VideomaticDbContextFixture Fixture { get; }
}
