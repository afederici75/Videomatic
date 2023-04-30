using Company.Videomatic.Application.Tests;

namespace Company.Videomatic.Infrastructure.Data.Tests;

public class DbContextTests<TDBContext> : DbContextTestsBase<TDBContext>
    where TDBContext : VideomaticDbContext
{
    protected DbContextTests(DbContextFixture<TDBContext> fixture)
        : base(fixture)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        //Fixture.SkipDeletingDatabase();
    }

    public DbContextFixture<TDBContext> Fixture { get; }

    [Theory]
    [InlineData(null)]
    public async Task CreatesSeededDbContext([FromServices] VideomaticDbContext db)
    {        
        var cnt = await db.Videos.CountAsync();
        cnt.Should().Be(YouTubeVideos.HintsCount);   // Should be seeded
    }

    [Theory]
    [InlineData(null)]
    public async Task CanStoreVideoWithAllCollectionsAndDeleteIt(
        [FromServices] VideomaticDbContext db)
    {
        var video = await VideoDataGenerator.CreateVideoFromFileAsync(YouTubeVideos.RickAstley_NeverGonnaGiveYouUp,
            nameof(Video.Thumbnails),
            nameof(Video.Transcripts),
            nameof(Video.Artifacts));

        video.Artifacts.Should().NotBeEmpty();
        video.Thumbnails.Should().NotBeEmpty();
        video.Transcripts.Should().NotBeEmpty();

        db.Add(video);
        db.SaveChanges();

        db.ChangeTracker.Clear();

        var record = await db.Videos
            .AsNoTracking()
            .Include(x => x.Transcripts)
            .ThenInclude(x => x.Lines)
            .Include(x => x.Thumbnails)
            .Include(x => x.Artifacts)
            .SingleAsync(v => v.Id == video.Id);

        record.Should().NotBeNull();
        record!.Id.Should().Be(video.Id);
        record!.Title.Should().Be(video.Title);
        record!.Description.Should().Be(video.Description);

        record!.Thumbnails.Should().BeEquivalentTo(video.Thumbnails);
        record!.Transcripts.Should().BeEquivalentTo(video.Transcripts);
        record!.Artifacts.Should().BeEquivalentTo(video.Artifacts);

        db.Remove(video);
        var res = await db.SaveChangesAsync();
        res.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData($"https://www.youtube.com/watch?v={YouTubeVideos.RickAstley_NeverGonnaGiveYouUp}", null, null)]
    [InlineData($"https://www.youtube.com/watch?v={YouTubeVideos.AldousHuxley_DancingShiva}", null, null)]
    [InlineData($"https://www.youtube.com/watch?v={YouTubeVideos.SwamiTadatmananda_WhySoManyGodsInHinduism}", null, null)]
    [InlineData($"https://www.youtube.com/watch?v={YouTubeVideos.HyonGakSunim_WhatIsZen}", null, null)]
    public async Task ImportVideoAndPersistToDb(
       string url,
       [FromServices] IVideoImporter importer,
       [FromServices] IRepositoryBase<Video> storage)
    {
        // Imports 
        Video video = await importer.ImportAsync(new Uri(url));

        video.Transcripts.Should().HaveCountGreaterThan(0);
        video.Transcripts.First().Lines.Should().HaveCountGreaterThan(0);
        video.Thumbnails.Should().HaveCountGreaterThan(0);

        // Persists
        await storage.UpdateAsync(video); // Will add a new record
        await storage.SaveChangesAsync();

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

        db.Remove(video);
        var res = await db.SaveChangesAsync();
        res.Should().BeGreaterThan(0);
    }
}