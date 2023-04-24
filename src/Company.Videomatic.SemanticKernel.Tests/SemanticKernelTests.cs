using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain;
using Company.Videomatic.Domain.Tests;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit.DependencyInjection;

namespace Company.Videomatic.SemanticKernel.Tests
{
    public class SemanticKernelTests
    {
        async Task<Video> GetHuxleysDancingShiva()
        {
            string json = await File.ReadAllTextAsync("TestData\\HuxleysDancingShiva.json") ?? string.Empty;

            var video = JsonConvert.DeserializeObject<Video>(json);
            return video!;
        }

        [Theory]
        [InlineData(null)]
        public async Task CanSummarizeTranscript([FromServices] IVideoAnalyzer videoAnalyzer)
        {
            var video = await GetHuxleysDancingShiva();

            var result = await videoAnalyzer.SummarizeTranscript(video.Transcripts.First());

            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Contain("Shiva");

            var words = result.Split();
            words.Length.Should().BeGreaterThan(20); // I get 60 with the result below.
            
            // Example result:
            // This article discusses the importance of symbols in Hinduism, specifically the symbol
            // of the dancing Shiva. The author argues that this symbol is both cosmic and psychological,
            // and that it represents the world of mass, energy, space, and time. The author also argues
            // that the symbol of the cross does not take into account the importance of contemplation.*/
        }

        [Theory]
        [InlineData(null)]
        public async Task CanReviewTranscript([FromServices] IVideoAnalyzer videoAnalyzer)
        {
            var video = await GetHuxleysDancingShiva();

            var result = await videoAnalyzer.ReviewTranscript(video.Transcripts.First());

            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Contain("1.");
            result.Should().Contain("2.");
            result.Should().Contain("3.");

            var words = result.Split();
            words.Length.Should().BeGreaterThan(20); // I get 60 with the result below.

            // Example result:
            // 1.The video discusses the Dancing Shiva, a symbol of Hinduism, and how it is a
            // more comprehensive and accurate symbol than the Christian cross.
            // 
            // 2.The video discusses the strengths of the Dancing Shiva as a symbol, including
            // its comprehensiveness and accuracy.
            // 
            // 3.The video discusses the weaknesses of the Christian cross as a symbol, including
            // its lack of comprehensiveness and accuracy.
        }
    }
}