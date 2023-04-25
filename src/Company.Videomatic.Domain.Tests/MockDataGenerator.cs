
namespace Company.Videomatic.Domain.Tests;

public static class MockDataGenerator
{
    [Flags]
    public enum VideoIncludes
    {
        None = 0,
        Thumbnails = 1, 
        Transcript = 2,
        Artifacts = 4,
        All = Thumbnails | Transcript | Artifacts   
    }

    /// <summary>
    /// This is a factory method that creates a new Video object with values of from 
    /// the musical video 'Never Gonna Give You Up'.
    /// It also allows you to optionally include 2 thumbnails and 1 transcript with 4 lines.
    /// </summary>
    public static Video CreateRickAstleyVideo(VideoIncludes includes = VideoIncludes.None)
    {
        var newVideo = new Video(
                   providerId: "YOUTUBE",
                   videoUrl: "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                   title: "Rick Astley - Never Gonna Give You Up (Official Music Video)",
                   description: "1,383,924,409 views  Oct 25, 2009  #NeverGonnaGiveYouUp #RickAstley #WheneverYouNeedSomebody [Cut for brevity]");

        if (includes.HasFlag(VideoIncludes.Thumbnails))
        {
            newVideo.AddThumbnails(
                new Thumbnail(
                    url: "https://i.ytimg.com/vi/dQw4w9WgXcQ/hqdefault.jpg",
                    resolution: ThumbnailResolution.High,
                    height: 360,
                    width: 480),

                new Thumbnail(
                     url: "https://i.ytimg.com/vi/dQw4w9WgXcQ/hqdefault.jpg",
                     resolution: ThumbnailResolution.High,
                     height: 360,
                     width: 480));            
        }

        if (includes.HasFlag(VideoIncludes.Transcript))
        {
            var transcript = new Transcript("US");
            newVideo.AddTranscripts(transcript);

            transcript.AddLines(
                new TranscriptLine(
                    text: "[Music]", 
                    duration: TimeSpan.FromSeconds(0), 
                    startsAt: TimeSpan.FromSeconds(22)),
                
                new TranscriptLine(
                    text: "you know the rules", 
                    duration: TimeSpan.FromSeconds(6), 
                    startsAt: TimeSpan.FromSeconds(22)),

                new TranscriptLine(
                    text: "[Music]", 
                    duration: TimeSpan.FromSeconds(12), 
                    startsAt: TimeSpan.FromSeconds(28)),

                new TranscriptLine(
                    text: "gotta make you understand", 
                    duration: TimeSpan.FromSeconds(4), 
                    startsAt: TimeSpan.FromSeconds(40))
                );
        }

        if (includes.HasFlag(VideoIncludes.Artifacts))
        {
            newVideo.AddArtifacts(
                new Artifact("Review", "The actual review would be here"),
                new Artifact("Summary", "The actual summary would be here")
                );
        }

        return newVideo;
    }
}
