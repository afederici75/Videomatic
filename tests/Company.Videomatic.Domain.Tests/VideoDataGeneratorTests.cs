using Company.Videomatic.Domain.Model;
using FluentAssertions;
using Newtonsoft.Json;

namespace Company.Videomatic.Domain.Tests;

public class VideoDataGeneratorTests
{
    [Fact(DisplayName = nameof(CreatesRickAstleyVideoWithoutDetails))]
    public async Task CreatesRickAstleyVideoWithoutDetails()
    {
        var video = await VideoDataGenerator.CreateVideoFromFileAsync(YouTubeVideos.RickAstley_NeverGonnaGiveYouUp);

        video.Transcripts.Count().Should().Be(0);
        video.Thumbnails.Count().Should().Be(0);
        video.Artifacts.Count().Should().Be(0);

        //video.ProviderId.Should().Be("YOUTUBE");
        video.Location.Should().Contain("youtube.com");
        video.Title.Should().Contain("Rick");
        video.Description.Should().Contain("#RickAstley");
        video.Description.Should().Contain("#NeverGonnaGiveYouUp");
        video.Description.Should().Contain("#WheneverYouNeedSomebody");
        video.Description.Should().Contain("#OfficialMusicVideo");
    }

    [Fact(DisplayName = nameof(CreatesRickAstleyVideoWithThumbsAndTranscript))]
    public async Task CreatesRickAstleyVideoWithThumbsAndTranscript()
    {
        var videoId = YouTubeVideos.RickAstley_NeverGonnaGiveYouUp;

        var video = await VideoDataGenerator.CreateVideoFromFileAsync(videoId,
            nameof(Video.Thumbnails),
            nameof(Video.Transcripts));

        var info = YouTubeVideos.GetInfoByVideoId(videoId);
        video.Thumbnails.Count().Should().Be(info.ThumbnailsCount);
        video.Transcripts.Count().Should().Be(info.TransctriptCount);
        video.Artifacts.Count().Should().Be(0);        
    }

    [Fact(DisplayName = nameof(CreatesRickAstleyVideoWithJustArtifacts))]
    public async Task CreatesRickAstleyVideoWithJustArtifacts()
    {
        var videoId = YouTubeVideos.RickAstley_NeverGonnaGiveYouUp;

        var video = await VideoDataGenerator.CreateVideoFromFileAsync(videoId,
            nameof(Video.Artifacts));

        video.Thumbnails.Count().Should().Be(0);
        video.Transcripts.Count().Should().Be(0);
        video.Artifacts.Count().Should().Be(2);
    }

    [Fact(DisplayName = nameof(SerializesProperlyWithJSONConver))]
    public async Task SerializesProperlyWithJSONConver()
    {
        var video = await VideoDataGenerator.CreateVideoFromFileAsync(YouTubeVideos.RickAstley_NeverGonnaGiveYouUp,
            nameof(Video.Transcripts),
            nameof(Video.Thumbnails),
            nameof(Video.Artifacts));

        var settings = JsonHelper.GetJsonSettings();
        var json = JsonConvert.SerializeObject(video, settings);
        
        //
        var deserializedVideo = JsonConvert.DeserializeObject<Video>(json, settings); 

        //
        var newJson = JsonConvert.SerializeObject(deserializedVideo, settings);
        newJson.Should().Be(json);
    }
}