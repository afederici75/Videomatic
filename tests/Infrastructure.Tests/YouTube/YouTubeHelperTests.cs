using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain.Aggregates.Artifact;
using Company.Videomatic.Domain.Aggregates.Playlist;
using Company.Videomatic.Domain.Aggregates.Transcript;
using Company.Videomatic.Domain.Aggregates.Video;
using Company.Videomatic.Infrastructure.YouTube;
using Microsoft.CognitiveServices.Speech.Transcription;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Xunit.Abstractions;

namespace Infrastructure.Tests.YouTube;

[Collection("DbContextTests")]
public class YouTubePlaylistsHelperTests : IClassFixture<DbContextFixture>
{
    public YouTubePlaylistsHelperTests(ITestOutputHelper output,
        DbContextFixture fixture,
        IRepository<Artifact> artifactRepository,
        IRepository<Transcript> transcriptRepository,
        IRepository<Playlist> playlistRepository,
        IRepository<Video> videoRepository,
        ISender sender,
        IYouTubeHelper helper)
    {
        Output = output ?? throw new ArgumentNullException(nameof(output));
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        ArtifactRepository = artifactRepository ?? throw new ArgumentNullException(nameof(artifactRepository));
        TranscriptRepository = transcriptRepository ?? throw new ArgumentNullException(nameof(transcriptRepository));
        PlaylistRepository = playlistRepository ?? throw new ArgumentNullException(nameof(playlistRepository));
        VideoRepository = videoRepository ?? throw new ArgumentNullException(nameof(videoRepository));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));
        Helper = helper ?? throw new ArgumentNullException(nameof(helper));

        Fixture.SkipDeletingDatabase = true;
    }

    public ITestOutputHelper Output { get; }
    public DbContextFixture Fixture { get; }
    public IRepository<Artifact> ArtifactRepository { get; }
    public IRepository<Transcript> TranscriptRepository { get; }
    public IRepository<Playlist> PlaylistRepository { get; }
    public IRepository<Video> VideoRepository { get; }
    public ISender Sender { get; }
    public IYouTubeHelper Helper { get; }

    [Theory]
    [InlineData("PLLdi1lheZYVJHCx7igCJIUmw6eGmpb4kb")] // Alternative Living, Sustainable Future
    [InlineData("PLOU2XLYxmsIKsEnF6CdfRK1Vd6XUn_QMu")] // Google I/O Keynote Films
    public async Task ImportVideosOfPlaylistInMemory(string playlistId)
    {        
        var ids = new List<VideoId>();
        await foreach (Video video in Helper.ImportVideosOfPlaylist(playlistId))
        {
            Output.WriteLine($"[{video.Id}]: {video.Name}");

            video.Id.Should().BeNull();
            video.Name.Should().NotBeEmpty();
        }
    }

    [Fact]
    public async Task ImportTranscriptionsOfSeededVideosInMemory()
    {
        await foreach (var transcript in Helper.ImportTranscriptions(new VideoId[] { 1, 2 }))
        {
            Output.WriteLine($"[{transcript.Language}]: {transcript.Lines.Count} Line(s))");
        }
    }

    [Theory]
    [InlineData(null, "PLLdi1lheZYVKkvX20ihB7Ay2uXMxa0Q5e")]
    public async Task ImportPlaylistFromYouTubeToJsonFile([FromServices] IYouTubeHelper helper, string playlistId)
    {
        List<Video> videos = new();
        await foreach (var video in helper.ImportVideosOfPlaylist(playlistId))
        {
            videos.Add(video);
        }

        // Saves a file with the list of videos
        var json = JsonSerializer.Serialize(videos);
        await File.WriteAllTextAsync($"Playlist-{playlistId}.json", json);
    }

    [Theory]
    [InlineData(null, new[] { "BBd3aHnVnuE" })]
    [InlineData(null, new[] { "4Y4YSpF6d6w", "tWZQPCU4LJI", "BBd3aHnVnuE", "BFfb2P5wxC0", "dQw4w9WgXcQ", "n1kmKpjk_8E" })]
    public async Task ImportVideosFromYouTubeToJsonFile([FromServices] IYouTubeHelper helper, string[] ids)
    {
        await foreach (var video in helper.ImportVideos(ids))
        {
            video.Name.Should().NotBeNullOrEmpty();
            video.Description.Should().NotBeNullOrEmpty();
            video.Location.Should().NotBeNullOrEmpty();
            video.Thumbnails.Should().HaveCount(5);
            video.Tags.Should().NotBeEmpty();

            video.Details.Provider.Should().Be(YouTubePlaylistsHelper.ProviderId);
            video.Details.VideoPublishedAt.Should().NotBe(DateTime.MinValue);
            video.Details.VideoOwnerChannelTitle.Should().NotBeEmpty();
            video.Details.VideoOwnerChannelId.Should().NotBeEmpty();


            // Saves a file with the video information
            var json = JsonSerializer.Serialize(video);
            await File.WriteAllTextAsync($"Video-{video.Details.ProviderVideoId}.json", json);
        }
    }

    [Fact]
    public async Task ImportTranscriptionsFromYouTubeVideosToJsonFile()
    {
        var videos = new Dictionary<VideoId, string>()
        {
            { 1, "n1kmKpjk_8E" }, // Aldous Huxley - The Dancing Shiva
            { 2, "BBd3aHnVnuE" }  // If Reality is NON-DUAL, Why are there so...
        };

        await foreach (var transcript in Helper.ImportTranscriptions(videos.Select(x => x.Key)))
        {
            string src = videos[transcript.VideoId];

            // Saves a file with the video information
            var json = JsonSerializer.Serialize(transcript);
            await File.WriteAllTextAsync($"Transcription-{src}.json", json);
        }
    }


    //[Theory(Skip = "Slow!!!")]
    [Theory]
    [InlineData("PLLdi1lheZYVJHCx7igCJIUmw6eGmpb4kb")] // Alternative Living, Sustainable Future
    [InlineData("PLOU2XLYxmsIKsEnF6CdfRK1Vd6XUn_QMu")] // Google I/O Keynote Films
    public async Task SLOWImportVideosOfPlaylistPlusTranscriptionInDatabase(string playlistId)
    {
        var ids = new List<VideoId>();

        var max = 1000;
        var pageSize = 50;
        await foreach (IEnumerable<Video> videos in Helper.ImportVideosOfPlaylist(playlistId).PageAsync(pageSize))
        {
            // Video
            await VideoRepository.AddRangeAsync(videos);
            ids.AddRange(videos.Select(x => x.Id));
            
            //Output.WriteLine($"[{video.Id}]: {video.Name}");            
            max -= pageSize;
            if (max<=0) break;
        }
        // Playlist 
        var playlist = await Sender.Send(new CreatePlaylistCommand($"Imported[{playlistId}]", "Imported from YouTube"));
        await Sender.Send(new LinkPlaylistToVideosCommand(playlist.Value.Id, ids));

        // Transcript
        foreach (var idPage in ids.Page(50))
        {
            var transcripts = await Helper.ImportTranscriptions(idPage).ToListAsync();
            await TranscriptRepository.AddRangeAsync(transcripts);

            Output.WriteLine($"[{transcripts.Count}] Transcript(s) {transcripts.Sum(x => x.Lines.Count)} Line(s))");
        }
    }

    //[Theory]
    //[InlineData("PLLdi1lheZYVJHCx7igCJIUmw6eGmpb4kb")] // Alternative Living, Sustainable Future
    //[InlineData("PLOU2XLYxmsIKsEnF6CdfRK1Vd6XUn_QMu")] // Google I/O Keynote Films
    //public async Task FASTImportVideosOfPlaylistPlusTranscriptionInDatabase(string playlistId)
    //{
    //    var ids = new List<VideoId>();
    //    await foreach (Video video in Helper.ImportVideosOfPlaylist(playlistId))
    //    {
    //        // Video
    //        await VideoRepository.AddAsync(video);
    //        ids.Add(video.Id);
    //
    //        Output.WriteLine($"[{video.Id}]: {video.Name}");
    //    }
    //
    //    // Transcript
    //    try
    //    {
    //        Transcript transcript = await Helper.ImportTranscriptions(new[] { video.Id }).SingleAsync();
    //        await TranscriptRepository.AddAsync(transcript);
    //
    //        Output.WriteLine($"[{transcript.Language}]: {transcript.Lines.Count} Line(s))");
    //    }
    //    catch (Exception ex)
    //    {
    //        Output.WriteLine($"[{video.Id} {video.Details.ProviderVideoId}]: {ex.Message}");
    //    }
    //    
    //}
}
