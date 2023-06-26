using System.Linq.Dynamic.Core;

namespace Infrastructure.Data.Tests;

[Collection("DbContextTests")]
public class DynamicLinqTests : IClassFixture<DbContextFixture>
{
    public DynamicLinqTests(
        DbContextFixture fixture,
        ISender sender,
        IMapper mapper)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));
        Mapper = mapper;
        Fixture.SkipDeletingDatabase = true;
    }

    public DbContextFixture Fixture { get; }
    public ISender Sender { get; }
    public IMapper Mapper { get; }

    [Fact]
    public async Task Query()
    {
        var proj = from pv in Fixture.DbContext.PlaylistVideos
                   select new
                   {
                       pv.Video.Id,
                       pv.Video.Title,
                       pv.Video.Description,
                       pv.Video.Location,
                       PlaylistCount = pv.Video.Playlists.Count,
                       ArtifactCount = pv.Video.Artifacts.Count,
                       ThumbnailCount = pv.Video.Thumbnails.Count,
                       TranscriptCount = pv.Video.Transcripts.Count,
                       TagCount = pv.Video.VideoTags.Count,
                       Thumbnail = pv.Video.Thumbnails.FirstOrDefault((t) => t.Resolution == (int)ThumbnailResolutionDTO.Default)
                   };

        var orderBy = $"{nameof(VideoDTO.TranscriptCount)} DESC, {nameof(VideoDTO.Id)}";

        var filter = @$"
{nameof(VideoDTO.TranscriptCount)} >= 0 &&
{nameof(VideoDTO.ThumbnailCount)} >= 0 &&
{nameof(VideoDTO.Title)}.Contains(""Huxley"") &&
{nameof(VideoDTO.Title)} == ""Aldous Huxley - The Dancing Shiva"" &&
{nameof(VideoDTO.TagCount)} >= 2 &&
Thumbnail.Id == 1";

        var results = await proj
            .OrderBy(orderBy)
            .Where(filter)
            .ToPageAsync(1, 10);

        results.Items.Should().HaveCount(1);
    }

    [Fact]
    public async Task QueryToDTO()
    {
        // Creates the projection
        var proj = from v in Fixture.DbContext.Videos
                   select new
                   {
                       v.Id,
                       v.Title,
                       v.Description,
                       v.Location,
                       Thumbnail = v.Thumbnails.FirstOrDefault((t) => t.Resolution == (int)ThumbnailResolutionDTO.Default)
                   };

        // Creates the filter
        var filter = @$"Description.Contains(""HUX"") || Title.Contains(""HUX"")";

        // Queries the database
        var results = await proj
            .Where(filter)
            .ToListAsync();

        // Transforms the results into DTOs manually
        var dtos = results
            .Select(v => new VideoDTO(
                Id: v.Id,
                Location: v.Location,
                Title: v.Title,
                Description: v.Description,
                Thumbnail: Mapper.Map<Thumbnail, ThumbnailDTO>(v.Thumbnail)
                ))
            .ToList();

        // Verifies
        results.Should().NotBeEmpty();
        dtos.Should().NotBeEmpty();
    }

    [Fact]
    public async Task QueryWithContains()
    {
        // Creates the projection
        var proj = from v in Fixture.DbContext.Videos
                   select new
                   {
                       v.Id,
                       v.Title,
                       v.Description,
                       v.Location,
                       Thumbnail = v.Thumbnails.FirstOrDefault((t) => t.Resolution == (int)ThumbnailResolutionDTO.Default)
                   };

        // Creates the filter
        var filter = $@"(Description.Contains(""HUX"") || Title.Contains(""HUX"")) && Id in (1,2)";

        // Queries the database
        var results = await proj
             .Where(filter)
             .ToPageAsync(1, 10);

        // Verifies
        results.Count.Should().Be(1);
    }
}
