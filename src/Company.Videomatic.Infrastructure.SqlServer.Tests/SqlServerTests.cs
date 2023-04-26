using Company.Videomatic.Domain;
using Company.Videomatic.Domain.Tests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using Xunit.DependencyInjection;

namespace Company.Videomatic.Infrastructure.SqlServer.Tests;

public class SqlServerTests : IClassFixture<VideomaticDbContextFixture>
{
    public SqlServerTests(VideomaticDbContextFixture fixture)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
    }

    public VideomaticDbContextFixture Fixture { get; }

    [Theory]
    [InlineData(null)]
    public async Task CanCreateDbContext([FromServices] VideomaticDbContext db)
    {
        var cnt = await db.Videos.CountAsync();
        cnt.Should().Be(0); 
    }

    [Theory]
    [InlineData(null)]
    public async Task CanStoreVideoWithThumbnailsAndTranscripts([FromServices] VideomaticDbContext db)
    {
        var video = await MockDataGenerator.CreateRickAstleyVideo(
            nameof(Video.Thumbnails),
            nameof(Video.Transcripts),
            nameof(Video.Artifacts));
        db.Add(video);
        db.SaveChanges();

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

        await db.DeleteVideoAsync(video.Id);
    }
}