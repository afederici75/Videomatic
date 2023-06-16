using Newtonsoft.Json;
using Xunit.Abstractions;

namespace Company.Videomatic.Infrastructure.YouTube.Tests;

public class YouTubePlaylistImporterTests
{
    private readonly ITestOutputHelper Output;

    public YouTubePlaylistImporterTests(ITestOutputHelper output)
    {
        Output = output ?? throw new ArgumentNullException(nameof(output));
    }

    [Theory]
    [InlineData(null, "https://www.youtube.com/playlist?list=PLLdi1lheZYVKkvX20ihB7Ay2uXMxa0Q5e")]
    public async Task ImportPlaylist([FromServices] IPlaylistImporter importer, string url)
    {
       throw new NotImplementedException();
       //var coll = await importer.ImportAsync(new Uri(url));
       //Assert.NotNull(coll);
       //
       //// Serializes        
       //var settings = JsonHelper.GetJsonSettings();
       //var json = JsonConvert.SerializeObject(coll, settings);
       //
       //// The files will be saved under \bin\Debug\net7.0\TestData.
       //var outputPath = VideoDataGenerator.FolderName;
       //if (!Directory.Exists(outputPath))
       //    Directory.CreateDirectory(outputPath);
       //
       //var fileName = $"{outputPath}\\Collection.json";
       //await File.WriteAllTextAsync(fileName, json); ;
       //
       //Output.WriteLine($"Written {fileName}:\n{json}");
    }
}
