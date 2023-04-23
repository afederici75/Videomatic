using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain;
using Company.Videomatic.Drivers.SqlServer.Tests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit.DependencyInjection;

namespace Company.Videomatic.Integration.Tests;

public class IntegrationTests : IClassFixture<VideomaticDbContextFixture>
{
    public IntegrationTests(VideomaticDbContextFixture fixture)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
    }

    public VideomaticDbContextFixture Fixture { get; }

    [Theory]
    [InlineData(null)]
    public async Task GetRickAsleyAndPersistToDb([FromServices] IVideoImporter importer)
    {
        var db = Fixture.DbContext;
        Video video = await importer.Import(
            new Uri("https://www.youtube.com/watch?v=dQw4w9WgXcQ&pp=ygUkcmljayBhc3RsZXkgbmV2ZXIgZ29ubmEgZ2l2ZSB5b3UgdXAg"));

        Assert.Equal("dQw4w9WgXcQ", video.ProviderId);
        Assert.Equal("Rick Astley - Never Gonna Give You Up (Official Music Video)", video.Title);

        db.Add(video);
        await db.SaveChangesAsync();

        db.ChangeTracker.Clear();

        var record = await db.Videos
            .AsNoTracking()
            .Include(x => x.Transcripts)
            .ThenInclude(x => x.Lines)
            .Include(x => x.Thumbnails)
            .FirstAsync(v => v.Id == video.Id);

        record.Should().NotBeNull();
        record!.Id.Should().Be(video.Id);
        record!.Title.Should().Be(video.Title);
        record!.Description.Should().Be(video.Description);

        record!.Thumbnails.Should().BeEquivalentTo(video.Thumbnails);
        record!.Transcripts.Should().BeEquivalentTo(video.Transcripts);
    }
}
