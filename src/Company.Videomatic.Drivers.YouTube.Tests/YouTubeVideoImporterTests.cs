using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Drivers.YouTube.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.DependencyInjection;

namespace Company.Videomatic.Drivers.YouTube.Tests;

public class YouTubeVideoImporterTests
{
    [Theory]
    [InlineData(null)]
    public async Task GetRickAsley([FromServices] IVideoImporter importer)
    {
        var video = await importer.Import(
            new Uri("https://www.youtube.com/watch?v=dQw4w9WgXcQ&pp=ygUkcmljayBhc3RsZXkgbmV2ZXIgZ29ubmEgZ2l2ZSB5b3UgdXAg"));

        Assert.Equal("dQw4w9WgXcQ", video.Id);
        Assert.Equal("Rick Astley - Never Gonna Give You Up (Official Music Video)", video.Title);        
    }
}
