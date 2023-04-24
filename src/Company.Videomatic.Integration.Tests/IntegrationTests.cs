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
    // Rick Astley - Never Gonna Give You Up (Official Music Video)
    [InlineData("https://www.youtube.com/watch?v=dQw4w9WgXcQ", null, null)]
    // Aldous Huxley - The Dancing Shiva
    [InlineData("https://www.youtube.com/watch?v=n1kmKpjk_8E", null, null)]
    // If Reality is NON-DUAL, Why are there so many GODS in Hinduism?
    [InlineData("https://www.youtube.com/watch?v=BBd3aHnVnuE", null, null)]
    // What's the PROOF that Vedanta is Correct? Science vs. Religion vs. Spirituality
    [InlineData("https://www.youtube.com/watch?v=Y0YXLVOdXtg", null, null)]
    public async Task ImportVideoAndPersistToDb(
        string url, 
        [FromServices] IVideoImporter importer,
        [FromServices] IVideoStorage storage)
    {        
        // Imports 
        Video video = await importer.Import(new Uri(url));        
        video.Transcripts.Should().HaveCountGreaterThan(0);
        video.Transcripts.First().Lines.Should().HaveCountGreaterThan(0);
        video.Thumbnails.Should().HaveCountGreaterThan(0);

        // Persists
        await storage.UpdateVideo(video);
        
        // Now reads
        video.Id.Should().BeGreaterThan(0);

        var db = Fixture.DbContext;
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
