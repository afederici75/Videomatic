using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Videomatic.Application.Tests.Mocks;

internal class MockVideoImporter : IVideoImporter
{
    public Task<Video> ImportAsync(Uri uri)
    {
        var video = new Video(
            providerId: "dQw4w9WgXcQ",
            videoUrl: "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
            title: "Just three sentences",
            description: "Just 3 sencences and a link to Rick Astley's official music video for “Never Gonna Give You Up”");

        var transcript = new Transcript("US");
        video.AddTranscripts(transcript);

        transcript.AddLines(
            new TranscriptLine(
                startsAt: TimeSpan.FromSeconds(0),
                duration: TimeSpan.FromSeconds(2),
                text: "First sentence..."),

            new TranscriptLine(
                startsAt: TimeSpan.FromSeconds(2),
                duration: TimeSpan.FromSeconds(3),
                text: "Second sentence..."),

            new TranscriptLine(
                startsAt: TimeSpan.FromSeconds(7),
                duration: TimeSpan.FromSeconds(1),
                text: "Third sentence..."));

        video.AddThumbnails(
            new Thumbnail(
                url: "https://i.ytimg.com/vi/dQw4w9WgXcQ/hqdefault.jpg",
                resolution: ThumbnailResolution.High,
                height: 1200,
                width: 1200),
            new Thumbnail(
                url: "https://i.ytimg.com/vi/dQw4w9WgXcQ/lqdefault.jpg",
                resolution: ThumbnailResolution.Standard,
                height: 200,
                width: 200));

        return Task.FromResult(video);
    }
}