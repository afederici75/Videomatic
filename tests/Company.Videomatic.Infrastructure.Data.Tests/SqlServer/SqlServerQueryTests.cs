namespace Company.Videomatic.Infrastructure.Data.Tests.SqlServer;

[Collection("DbContextTests")]
public class SqlServerQueryTests : IClassFixture<SqlServerDbContextFixture>
{
    public SqlServerQueryTests(
        SqlServerDbContextFixture fixture,
        ISender sender,
        IMapper mapper)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));
        Mapper = mapper;
        Fixture.SkipDeletingDatabase = true;
    }

    public SqlServerDbContextFixture Fixture { get; }
    public ISender Sender { get; }
    public IMapper Mapper { get; }

    [Fact]
    public async Task QueryExample()
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
            Thumbnail = pv.Video.Thumbnails.FirstOrDefault((Thumbnail t) => t.Resolution == ThumbnailResolution.Default)
        };

        #region Order and Filter
        var orderBy = new OrderBy(
            Items: new OrderByItem[]
            {
                new (nameof(VideoDTO.TranscriptCount), OrderDirection.Desc),
                new (nameof(VideoDTO.Id), OrderDirection.Desc)
            });

        var filter = new Filter(
            SearchText: null,
            Ids: new long[] { 1, 2, 333, 444 },
            Items: new FilterItem[]
            {
                new (nameof(VideoDTO.TranscriptCount), FilterType.GreaterThan, "0"),
                new (nameof(VideoDTO.ThumbnailCount), FilterType.GreaterThan, "0"),
                new (nameof(VideoDTO.Title), FilterType.Contains, "Huxley"),
                new (nameof(VideoDTO.Title), FilterType.Equals, "Aldous Huxley - The Dancing Shiva"),
                new (nameof(VideoDTO.TagCount), FilterType.Equals, "2"),
                new ("Thumbnail.Id", FilterType.Equals, "1")
            }
            );
        filter = new Filter();
        #endregion  

        throw new NotImplementedException();
        //var results = await proj
        //    .ApplyOrderBy(orderBy)
        //    .ApplyFilters(filter, new[] { nameof(VideoDTO.Title), nameof(VideoDTO.Description) })
        //    .ToPageAsync(new Paging(1,10));
        //
        //results.Items.Should().NotBeEmpty();        
    }

    [Fact]
    public async Task QueryExample2()
    {
        // Creates the projection
        var proj = from v in Fixture.DbContext.Videos
                   select new
                   {
                       v.Id,
                       v.Title,
                       v.Description,
                       v.Location,
                       Thumbnail = v.Thumbnails.FirstOrDefault((Thumbnail t) => t.Resolution == ThumbnailResolution.Default)
                   };

        // Creates the filter
        var filter = new Filter(SearchText: "HUX", Ids: new long[] { 1, 2, 3, 444, 555, 666 });

        // Queries the database
        var results = await proj
            .ApplyFilters(filter, new[] { nameof(VideoDTO.Title), nameof(VideoDTO.Description) })
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
    public async Task QueryExample3()
    {
        // Creates the projection
        var proj = from v in Fixture.DbContext.Videos
                   select new
                   {
                       v.Id,
                       v.Title,
                       v.Description,
                       v.Location,
                       Thumbnail = v.Thumbnails.FirstOrDefault((Thumbnail t) => t.Resolution == ThumbnailResolution.Default)
                   };

        // Creates the filter
        var filter = new Filter(
            SearchText: "HUX",
            Ids: new long[] { 1, 2, 3, 444, 555, 666 });

        var paging = new Paging(1, 10);

        throw new Exception();
        // Queries the database
       //var results = await proj
       //    .ApplyFilters(filter, new[] { nameof(VideoDTO.Title), nameof(VideoDTO.Description) })
       //    .ToPageAsync(1, 10, v => new VideoDTO(
       //        Id: v.Id,
       //        Location: v.Location,
       //        Title: v.Title,
       //        Description: v.Description,
       //        Thumbnail: Mapper.Map<Thumbnail, ThumbnailDTO>(v.Thumbnail)
       //        ));
       //
       //// Verifies
       //results.Count.Should().Be(1);
    }
}
