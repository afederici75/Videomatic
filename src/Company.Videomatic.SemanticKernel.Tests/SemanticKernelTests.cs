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
            
            return JsonConvert.DeserializeObject<Video>(json);
        }

        [Theory]
        [InlineData(null)]
        public async Task CanSummarizeTranscript([FromServices] IVideoAnalyzer videoAnalyzer)
        {
            if (videoAnalyzer is null)
            {
                throw new ArgumentNullException(nameof(videoAnalyzer));
            }

            var video = await GetHuxleysDancingShiva();

            var result = await videoAnalyzer.SummarizeTranscript(video.Transcripts.First());

            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Contain("Shiva");

            // Example result:
            // This article discusses the importance of symbols in Hinduism, specifically the symbol
            // of the dancing Shiva. The author argues that this symbol is both cosmic and psychological,
            // and that it represents the world of mass, energy, space, and time. The author also argues
            // that the symbol of the cross does not take into account the importance of contemplation.*/
        }
    }
}