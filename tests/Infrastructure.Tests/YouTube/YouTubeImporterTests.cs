using Infrastructure.YouTube;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Options;
using System.Security.Cryptography.X509Certificates;

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
        Options = options?.Value ?? throw new ArgumentException(nameof(options.Value));
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

    readonly ImportOptions ImportOptions = new (ExecuteImmediate: true);

    [Theory]
    // TODO: This playlist takes too long to import for tests after I added the AI stuff.
    //[InlineData(new [] { "PLLdi1lheZYVJHCx7igCJIUmw6eGmpb4kb" }, 270, default)] // Alternative Living, Sustainable Future
    [InlineData(new [] { "PLOU2XLYxmsIKsEnF6CdfRK1Vd6XUn_QMu" }, 5, null)] // Google I/O Keynote Films
    public async Task ImportVideosOfPlaylists(string[] playlistIds, int expectedCount, [FromServices] CancellationToken? cancellationToken)
    {
        var origVideoCount = await VideoRepository.CountAsync();
        var origPlaylistCount = await PlaylistRepository.CountAsync();

        await Importer.ImportPlaylistsAsync(playlistIds, ImportOptions, cancellationToken ?? CancellationToken.None);

        (await PlaylistRepository.CountAsync()).Should().Be(origPlaylistCount + 1);
        (await VideoRepository.CountAsync()).Should().Be(origVideoCount + expectedCount);
    }
   
    [Theory]
    [InlineData(new[] { "BBd3aHnVnuE" }, 0, null)]
    [InlineData(new[] { "4Y4YSpF6d6w", "https://youtube.com/watch?v=tWZQPCU4LJI" }, 2, null)]
    // TODO: figure out why this blows up by 1! - Expected newCount to be 282, but found 281 (difference of -1).
    //[InlineData(new[] { "https://www.youtube.com/watch?v=oqet-wVkBoA&list=PLLdi1lheZYVIvA_YctNtIhbp_KJ54w7yL&index=3&pp=gAQBiAQB",
    //                    "https://www.youtube.com/watch?v=8Y_HlIPRUc8", 
    //                    "https://www.youtube.com/watch?v=5DrM90dg5t4&list=PLLdi1lheZYVLPVCrATDJUF_Pumjedwvyi" }, 3, null)]
    public async Task ImportVideosWithIdsOrUrls(string[] ids, int expectedAdded, CancellationToken? cancellationToken)
    {
        var count = await VideoRepository.CountAsync();

        await Importer.ImportVideosAsync(ids, null, this.ImportOptions, cancellationToken ?? CancellationToken.None);

        var newCount = await VideoRepository.CountAsync();
        newCount.Should().Be(count + expectedAdded);
    }

#pragma warning disable xUnit1004 // Test methods should not be skipped
    [Fact(Skip = "I need to recreate the google credentials (svc acct)")]
#pragma warning restore xUnit1004 // Test methods should not be skipped
    public async Task AuthenticateGoogleOAuth()
    {
        String serviceAccountEmail = "videomaticserviceaccount-422@videomatic-384421.iam.gserviceaccount.com";

        var certificate = new X509Certificate2(@"googlekey.p12", "notasecret", X509KeyStorageFlags.Exportable);

        var credential = new ServiceAccountCredential(
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

        response.Should().NotBeNull();  
    }
}
