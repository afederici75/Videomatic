using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Infrastructure.TestData;

public class TestDataVideoImporter : IVideoImporter
{
    public async Task<Video> ImportAsync(Uri uri)
    {
        var info = YouTubeVideos.GetInfoByUri(uri);

        var video = await VideoDataGenerator.CreateVideoFromFile(info.VideoId,
            nameof(Video.Thumbnails),
            nameof(Video.Transcripts));

        return video;
    }
}