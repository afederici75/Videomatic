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
        
        //Fixture.SkipDeletingDatabase = true;
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
    [InlineData(new [] { "PLLdi1lheZYVJHCx7igCJIUmw6eGmpb4kb" }, 271, null)] // Alternative Living, Sustainable Future
    [InlineData(new [] { "PLOU2XLYxmsIKsEnF6CdfRK1Vd6XUn_QMu" }, 5, null)] // Google I/O Keynote Films
    public async Task ImportVideosOfPlaylists(string[] playlistIds, int expectedCount, CancellationToken cancellationToken)
    {
        var videoCount = await VideoRepository.CountAsync(); 
        var playlistCount = await PlaylistRepository.CountAsync();

        await Importer.ImportPlaylistsAsync(playlistIds, cancellationToken);

        var newPlaylistCount = await PlaylistRepository.CountAsync();
        newPlaylistCount.Should().Be(playlistCount + 1);

        // TODO: refactor
        var max = 100;
        do
        {
            max--;
                
            var newVideoCount = await VideoRepository.CountAsync();
            if (newVideoCount == videoCount + expectedCount)
            {
                break;
            }
            
            await Task.Delay(500);
        } while (max-- > 0);

        if (max == 0)
        {
            throw new Exception("Videos were not imported");
        }

        var finalVideoCount = await VideoRepository.CountAsync();
        finalVideoCount.Should().Be(videoCount + expectedCount);        
    }

    [Fact]
    public async Task ImportTranscriptionsOfSeededVideosInMemory()
    {
        var count = await TranscriptRepository.CountAsync();

        await Importer.ImportTranscriptionsAsync(new VideoId[] { 1, 2 });

        var newCount = await TranscriptRepository.CountAsync();
        newCount.Should().Be(count);
    }
   

    [Theory]
    [InlineData(new[] { "BBd3aHnVnuE" }, null)]
    [InlineData(new[] { "4Y4YSpF6d6w", "tWZQPCU4LJI", "BBd3aHnVnuE", "BFfb2P5wxC0", "dQw4w9WgXcQ", "n1kmKpjk_8E" }, null)]
    public async Task ImportVideosFromYouTube(string[] ids, CancellationToken cancellationToken)
    {
        var count = await VideoRepository.CountAsync();

        await Importer.ImportVideosAsync(ids, null, cancellationToken);

        var newCount = await VideoRepository.CountAsync();
        newCount.Should().Be(count + ids.Length);
    }   


    [Theory(Skip = "Slow to execute. use only when needed.")]
    //[Theory]
    [InlineData("Alternative Living, Sustainable Future", "PLLdi1lheZYVJHCx7igCJIUmw6eGmpb4kb")] // 
    [InlineData("Google I/O Keynote Films", "PLOU2XLYxmsIKsEnF6CdfRK1Vd6XUn_QMu")] // 
    [InlineData("Nice vans", "PLLdi1lheZYVLxuwEIB09Bub14y1W83iHo")] // 
    public async Task SLOWImportVideosOfPlaylistPlusTranscriptionInDatabase(string playlistName, string playlistId)
    {
        await Importer.ImportPlaylistsAsync(new[] { playlistId });
    }


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
