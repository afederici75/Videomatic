using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Infrastructure.Data;

public class MockVideoImporter : IVideoImporter
{
    public async Task<Video> ImportAsync(Uri uri)
    {
        var info = YouTubeVideos.GetInfoByUri(uri);

        var video = await VideoDataGenerator.CreateVideoFromFileAsync(info.ProviderVideoId,
            nameof(Video.Thumbnails),
            nameof(Video.Transcripts));

        return video;
    }
}