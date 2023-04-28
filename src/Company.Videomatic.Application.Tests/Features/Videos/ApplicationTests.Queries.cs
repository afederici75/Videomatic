using Company.SharedKernel.Specifications;
using Company.Videomatic.Application.Features.Videos.Queries.GetVideos;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace Company.Videomatic.Application.Tests.Features.Videos;

public partial class ApplicationTests
{    
    [Theory]
    [InlineData(null)]
    public virtual async Task QueryAllVideoDTOs(
            [FromServices] ISender sender)
    {
        var cmd = new GetVideosDTOQuery();
        var response = await sender.Send(cmd);
        Output.WriteLine(JsonHelper.Serialize(response));

        // Should be all videos
        response.Items.Should().HaveCount(YouTubeVideos.HintsCount);

        var lastId = 0;
        foreach (var item in response.Items)
        {
            // Check they are in sequence by Id
            item.Id.Should().BeGreaterThan(lastId);
            lastId = item.Id;

            // Check they have basic properties
            item.Title.Should().NotBeNullOrWhiteSpace();    
            item.Description.Should().NotBeNullOrWhiteSpace();
            item.ProviderId.Should().NotBeNullOrWhiteSpace();
            item.VideoUrl.Should().NotBeNullOrWhiteSpace();
            item.ProviderId.Should().NotBeNullOrWhiteSpace();            

            // Check they don't include anything
            item.Artifacts.Should().BeEmpty();
            item.Thumbnails.Should().BeEmpty();
            item.Transcripts.Should().BeEmpty();            
        }
    }

    [Theory]
    [InlineData(null)]
    public virtual async Task QueryOnlySome(
            [FromServices] ISender sender)
    {
        var cmd = new GetVideosDTOQuery(
            ProviderVideoIdPrefix: "B",
            VideoUrlPrefix: "http"
            );
        
        var response = await sender.Send(cmd);
        Output.WriteLine(JsonHelper.Serialize(response));

        // Should be all videos
        response.Items.Should().HaveCount(2); // 2 videos: BBd3aHnVnuE and BFfb2P5wxC0        
    }
}
