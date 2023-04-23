using Company.Videomatic.Application.Abstractions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.DependencyInjection;

namespace Company.Videomatic.Application.Tests;

public class VideoLinksTests
{
    [Theory]
    [InlineData(null, null)]
    public async Task ImportFromMockImporter(
        [FromServices] IVideoImporter importer,
        [FromServices] IVideoStorage storage)
    {
        var video = await importer.Import(new Uri("http://nowhere.com?32kjjkebewkjbew"));
        video.Should().NotBeNull();
        video.Id.Should().BeLessThanOrEqualTo(0);

        var updateResult = await storage.UpdateVideo(video);
        updateResult.Should().BeGreaterThan(0);

        var deleteResult = await storage.DeleteVideo(video.Id);
        deleteResult.Should().BeTrue();
    }
}
