using Company.Videomatic.Infrastructure.Data.Tests.Base;
using Microsoft.EntityFrameworkCore;

namespace Company.Videomatic.Infrastructure.Data.Tests;

[Collection("DbContextTests")]
public abstract class DbContextTestsBase : IClassFixture<DbContextFixture>
{
    protected DbContextTestsBase(DbContextFixture fixture)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        //Fixture.SkipDeletingDatabase();
    }

    public DbContextFixture Fixture { get; }

    [Fact(DisplayName =  nameof(CreatesSeededDbContext))]
    public async Task CreatesSeededDbContext()
    {
        var cnt = await Fixture.DbContext.Videos.CountAsync();
        cnt.Should().Be(YouTubeVideos.HintsCount);   // Should be seeded
    }

    [Fact(DisplayName = nameof(CanStoreVideoWithAllCollectionsAndDeleteIt))]
    public async Task CanStoreVideoWithAllCollectionsAndDeleteIt()
    {
        throw new NotImplementedException();

        //var video = await VideoDataGenerator.CreateVideoFromFileAsync(YouTubeVideos.RickAstley_NeverGonnaGiveYouUp,
        //    nameof(Video.Thumbnails),
        //    nameof(Video.Transcripts),
        //    nameof(Video.Artifacts));
        //
        //video.Artifacts.Should().NotBeEmpty();
        //video.Thumbnails.Should().NotBeEmpty();
        //video.Transcripts.Should().NotBeEmpty();
        //
        //Fixture.DbContext.Add(video);
        //Fixture.DbContext.SaveChanges();
        //
        //Fixture.DbContext.ChangeTracker.Clear();
        //
        //var record = await Fixture.DbContext.Videos
        //    .AsNoTracking()
        //    .Include(x => x.Transcripts)
        //    .ThenInclude(x => x.Lines)
        //    .Include(x => x.Thumbnails)
        //    .Include(x => x.Artifacts)
        //    .SingleAsync(v => v.Id == video.Id);
        //
        //record.Should().NotBeNull();
        //record!.Id.Should().Be(video.Id);
        //record!.Title.Should().Be(video.Title);
        //record!.Description.Should().Be(video.Description);
        //
        //record!.Thumbnails.Should().BeEquivalentTo(video.Thumbnails);
        //record!.Transcripts.Should().BeEquivalentTo(video.Transcripts);
        //record!.Artifacts.Should().BeEquivalentTo(video.Artifacts);
        //
        //Fixture.DbContext.Remove(video);
        //var res = await Fixture.DbContext.SaveChangesAsync();
        //res.Should().BeGreaterThan(0);
    }


}