using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain.Model;
using Company.Videomatic.Infrastructure.SqlServer.Tests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit.DependencyInjection;

namespace Company.Videomatic.Integration.Tests;

[Collection("Sequence")]
public class IntegrationTests : IClassFixture<VideomaticDbContextFixture>
{
    public IntegrationTests(VideomaticDbContextFixture fixture)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        //Fixture.SkipDeletingDatabase();
    }

    public VideomaticDbContextFixture Fixture { get; }

   
}