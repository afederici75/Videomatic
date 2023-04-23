
namespace Company.Videomatic.Domain.Tests;

public static class MockDataGenerator
{
    /// <summary>
    /// This is a factory method that creates a new Video object with values of from 
    /// the musical video 'Never Gonna Give You Up'.
    /// It also allows you to optionally include 2 thumbnails and 1 transcript with 4 lines.
    /// </summary>
    public static Video CreateRickAstleyVideo(bool includeThumbnails, bool includeTranscript)
    {
        var newVideo = new Video(
                   providerId: "YOUTUBE",
                   videoUrl: "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                   title: "Rick Astley - Never Gonna Give You Up (Official Music Video)",
                   description: "1,383,924,409 views  Oct 25, 2009  #NeverGonnaGiveYouUp #RickAstley #WheneverYouNeedSomebody [Cut for brevity]");

        if (includeThumbnails)
        {
            newVideo.AddThumbnails(
                new Thumbnail(
                    video: newVideo,
                    url: "https://i.ytimg.com/vi/dQw4w9WgXcQ/hqdefault.jpg",
                    resolution: ThumbnailResolution.High,
                    height: 360,
                    width: 480),

                new Thumbnail(
                     video: newVideo,
                     url: "https://i.ytimg.com/vi/dQw4w9WgXcQ/hqdefault.jpg",
                     resolution: ThumbnailResolution.High,
                     height: 360,
                     width: 480));            
        }

        if (includeTranscript)
        {
            var transcript = new Transcript(newVideo, "US");
            newVideo.AddTranscripts(transcript);

            transcript.AddLines(
                new TranscriptLine(
                    transcript, 
                    text: "[Music]", 
                    duration: TimeSpan.FromSeconds(0), 
                    startsAt: TimeSpan.FromSeconds(22)),
                
                new TranscriptLine(
                    transcript, 
                    text: "you know the rules", 
                    duration: TimeSpan.FromSeconds(6), 
                    startsAt: TimeSpan.FromSeconds(22)),

                new TranscriptLine(
                    transcript, 
                    text: "[Music]", 
                    duration: TimeSpan.FromSeconds(12), 
                    startsAt: TimeSpan.FromSeconds(28)),

                new TranscriptLine(
                    transcript, 
                    text: "gotta make you understand", 
                    duration: TimeSpan.FromSeconds(4), 
                    startsAt: TimeSpan.FromSeconds(40))
                );
        }

        return newVideo;
    }
}
