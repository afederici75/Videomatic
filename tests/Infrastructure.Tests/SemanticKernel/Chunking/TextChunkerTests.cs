using Application.Specifications;
using Microsoft.SemanticKernel.SemanticFunctions.Partitioning;

namespace Infrastructure.Tests.SemanticKernel;

[Collection("DbContextTests")]
public class TextChunkerTests : IClassFixture<DbContextFixture>
{
    public TextChunkerTests(
        IRepository<Transcript> transcriptRepository,
        IRepository<Video> videoRepository,
        IVideoImporter videoImporter)
    {
        TranscriptRepository = transcriptRepository ?? throw new ArgumentNullException(nameof(transcriptRepository));
        VideoRepository = videoRepository ?? throw new ArgumentNullException(nameof(videoRepository));
        VideoImporter = videoImporter ?? throw new ArgumentNullException(nameof(videoImporter));
    }

    public IRepository<Transcript> TranscriptRepository { get; }
    public IRepository<Video> VideoRepository { get; }
    public IVideoImporter VideoImporter { get; }

    [Fact]
    public async Task SplitHuxleysDancingShiva()
    {
        var ids = new string[] { YouTubeVideos.AldousHuxley_DancingShiva2 };

        await VideoImporter.ImportVideosAsync(ids, options: new(ExecuteImmediate: true));

        var video = await VideoRepository.SingleOrDefaultAsync(
            new QueryVideos.ByProviderItemId("YOUTUBE", ids));
        video.Should().NotBeNull();

        var t = await TranscriptRepository.SingleOrDefaultAsync(
            new QueryTranscripts.ByVideoId(video!.Id));
        t.Should().NotBeNull();

        var text = t!.GetFullText();
        
        // The following splits nicely!
        // TODO: tweak so the fragments overlap a little bit
        var result2 = SemanticTextPartitioner.SplitPlainTextLines(text, 200);        

        result2.Should().NotBeEmpty();
        result2.Count.Should().Be(8);
    }
}