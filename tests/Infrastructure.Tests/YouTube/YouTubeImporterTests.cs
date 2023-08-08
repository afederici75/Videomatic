using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain.Aggregates.Artifact;
using Company.Videomatic.Domain.Aggregates.Playlist;
using Company.Videomatic.Domain.Aggregates.Transcript;
using Company.Videomatic.Domain.Aggregates.Video;
using Company.Videomatic.Infrastructure.YouTube;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Options;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Infrastructure.Tests.YouTube;

[Collection("DbContextTests")]
public class YouTubeImporterTests : IClassFixture<DbContextFixture>
{
    public YouTubeImporterTests(ITestOutputHelper output,
        DbContextFixture fixture,
        IOptions<YouTubeOptions> options,
        IRepository<Artifact> artifactRepository,
        IRepository<Transcript> transcriptRepository,
        IRepository<Playlist> playlistRepository,
        IRepository<Video> videoRepository,
        ISender sender,
        IVideoImporter importer)
    {
        Output = output ?? throw new ArgumentNullException(nameof(output));
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        Options = options?.Value ?? throw new ArgumentException(nameof(options));
        ArtifactRepository = artifactRepository ?? throw new ArgumentNullException(nameof(artifactRepository));
        TranscriptRepository = transcriptRepository ?? throw new ArgumentNullException(nameof(transcriptRepository));
        PlaylistRepository = playlistRepository ?? throw new ArgumentNullException(nameof(playlistRepository));
        VideoRepository = videoRepository ?? throw new ArgumentNullException(nameof(videoRepository));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));
        Importer = importer ?? throw new ArgumentNullException(nameof(importer));
        
        Fixture.SkipDeletingDatabase = true;
    }

    public ITestOutputHelper Output { get; }
    public DbContextFixture Fixture { get; }
    public YouTubeOptions Options { get; }
    public IRepository<Artifact> ArtifactRepository { get; }
    public IRepository<Transcript> TranscriptRepository { get; }
    public IRepository<Playlist> PlaylistRepository { get; }
    public IRepository<Video> VideoRepository { get; }
    public ISender Sender { get; }
    public IVideoImporter Importer { get; }

    [Theory]
    [InlineData("PLLdi1lheZYVJHCx7igCJIUmw6eGmpb4kb")] // Alternative Living, Sustainable Future
    [InlineData("PLOU2XLYxmsIKsEnF6CdfRK1Vd6XUn_QMu")] // Google I/O Keynote Films
    public async Task ImportVideosOfPlaylistInMemory(string playlistId)
    {        
        var ids = new List<VideoId>();
        await foreach (Video video in Importer.ImportVideosOfPlaylist(playlistId))
        {
            Output.WriteLine($"[{video.Id}]: {video.Name}");

            video.Id.Should().BeNull();
            video.Name.Should().NotBeEmpty();
        }
    }

    [Fact]
    public async Task ImportTranscriptionsOfSeededVideosInMemory()
    {
        await foreach (var transcript in Importer.ImportTranscriptions(new VideoId[] { 1, 2 }))
        {
            Output.WriteLine($"[{transcript.Language}]: {transcript.Lines.Count} Line(s))");
        }
    }

    [Theory]
    [InlineData(null, "PLLdi1lheZYVKkvX20ihB7Ay2uXMxa0Q5e")]
    public async Task ImportPlaylistFromYouTubeToJsonFile([FromServices] IVideoImporter helper, string playlistId)
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
    public async Task ImportVideosFromYouTubeToJsonFile([FromServices] IVideoImporter helper, string[] ids)
    {
        await foreach (var video in helper.ImportVideos(ids))
        {
            video.Name.Should().NotBeNullOrEmpty();
            video.Description.Should().NotBeNullOrEmpty();
            video.Location.Should().NotBeNullOrEmpty();
            video.Thumbnails.Should().HaveCount(5);
            video.Tags.Should().NotBeEmpty();

            video.Details.Provider.Should().Be(YouTubeVideoHostingProvider.ProviderId);
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

        await foreach (var transcript in Importer.ImportTranscriptions(videos.Select(x => x.Key)))
        {
            string src = videos[transcript.VideoId];

            // Saves a file with the video information
            var json = JsonSerializer.Serialize(transcript);
            await File.WriteAllTextAsync($"Transcription-{src}.json", json);
        }
    }


    [Theory(Skip = "Slow to execute. use only when needed.")]
    //[Theory]
    [InlineData("Alternative Living, Sustainable Future", "PLLdi1lheZYVJHCx7igCJIUmw6eGmpb4kb")] // 
    [InlineData("Google I/O Keynote Films", "PLOU2XLYxmsIKsEnF6CdfRK1Vd6XUn_QMu")] // 
    [InlineData("Nice vans", "PLLdi1lheZYVLxuwEIB09Bub14y1W83iHo")] // 
    public async Task SLOWImportVideosOfPlaylistPlusTranscriptionInDatabase(string playlistName, string playlistId)
    {
        var ids = new List<VideoId>();

        var max = 1000;
        var pageSize = 50;
        await foreach (IEnumerable<Video> videos in Importer.ImportVideosOfPlaylist(playlistId).PageAsync(pageSize))
        {
            // Video
            await VideoRepository.AddRangeAsync(videos);
            ids.AddRange(videos.Select(x => x.Id));
            
            //Output.WriteLine($"[{video.Id}]: {video.Name}");            
            max -= pageSize;
            if (max<=0) break;
        }
        // Playlist 
        var playlist = await Sender.Send(new CreatePlaylistCommand(playlistName, $"Imported from YouTube playlist '{playlistId}'."));
        await Sender.Send(new LinkPlaylistToVideosCommand(playlist.Value.Id, ids));

        // Transcript
        foreach (var idPage in ids.Page(50))
        {
            var transcripts = await Importer.ImportTranscriptions(idPage).ToListAsync();
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

    [Fact(Skip = "I need to recreate the google credentials (svc acct)")]
    public async Task AuthenticateGoogleOAuth()
    {
        String serviceAccountEmail = "videomaticserviceaccount-422@videomatic-384421.iam.gserviceaccount.com";

        var certificate = new X509Certificate2(@"googlekey.p12", "notasecret", X509KeyStorageFlags.Exportable);

        ServiceAccountCredential credential = new ServiceAccountCredential(
           new ServiceAccountCredential.Initializer(serviceAccountEmail)
           {
               Scopes = new[] { YouTubeService.Scope.Youtube }
           }.FromCertificate(certificate));

        // Create the service.
        var service = new YouTubeService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "API Sample",
        });

        var request = service.Playlists.List("snippet");
        request.ChannelId = "PLLdi1lheZYVKkvX20ihB7Ay2uXMxa0Q5e"; // My philosophy playlist for now
        request.MaxResults = 50;
        //request.Mine = false;
        Google.Apis.YouTube.v3.Data.PlaylistListResponse response = await request.ExecuteAsync();
    }
}
