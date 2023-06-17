using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Infrastructure.SemanticKernel.Tests
{
    public class VideoAnalyzerTests
    {
        [Theory(DisplayName = nameof(CanSummarizeTranscript))]
        [InlineData(null)]
        public async Task CanSummarizeTranscript([FromServices] IVideoAnalyzer videoAnalyzer)
        {
            throw new NotImplementedException();

            //var video = await VideoDataGenerator.CreateVideoFromFileAsync(YouTubeVideos.AldousHuxley_DancingShiva,
            //    nameof(Video.Transcripts) 
            //    );
            //
            //var result = await videoAnalyzer.SummarizeVideoAsync(video);
            //
            //result.Text.Should().NotBeNullOrWhiteSpace();
            ////result.Text.Should().Contain("Shiva");
            //
            //var words = result.Text?.Split();
            //words!.Length.Should().BeGreaterThan(20); // I get 60 with the result below.
            //
            //// Example result:
            //// This article discusses the importance of symbols in Hinduism, specifically the symbol
            //// of the dancing Shiva. The author argues that this symbol is both cosmic and psychological,
            //// and that it represents the world of mass, energy, space, and time. The author also argues
            //// that the symbol of the cross does not take into account the importance of contemplation.*/
        }

        [Theory(DisplayName = nameof(CanReviewTranscript))]
        [InlineData(null)]
        public async Task CanReviewTranscript([FromServices] IVideoAnalyzer videoAnalyzer)
        {
            throw new NotImplementedException();
            
            //var video = await VideoDataGenerator.CreateVideoFromFileAsync(YouTubeVideos.AldousHuxley_DancingShiva,
            //    nameof(Video.Transcripts)
            //    );
            //
            //Artifact result = await videoAnalyzer.ReviewVideoAsync(video);
            //
            //result.Text.Should().NotBeNullOrWhiteSpace();
            //result.Text.Should().Contain("1.");
            //result.Text.Should().Contain("2.");
            //result.Text.Should().Contain("3.");
            //
            //var words = result.Text?.Split();
            //words?.Length.Should().BeGreaterThan(20); // I get 60 with the result below.
            //
            //// Example result:
            //// 1.The video discusses the Dancing Shiva, a symbol of Hinduism, and how it is a
            //// more comprehensive and accurate symbol than the Christian cross.
            //// 
            //// 2.The video discusses the strengths of the Dancing Shiva as a symbol, including
            //// its comprehensiveness and accuracy.
            //// 
            //// 3.The video discusses the weaknesses of the Christian cross as a symbol, including
            //// its lack of comprehensiveness and accuracy.
        }
    }
}