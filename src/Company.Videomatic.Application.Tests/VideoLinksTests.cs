using Company.Videomatic.Application.Abstractions;
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
    [InlineData(null)]
    public async Task ImportFromMockImporter(
        [FromServices] IVideoImporter importer)
    {
        var video = await importer.Import(new Uri("http://nowhere.com?32kjjkebewkjbew"));
    }

}
