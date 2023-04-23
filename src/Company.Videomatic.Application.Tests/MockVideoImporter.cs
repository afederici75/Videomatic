using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Videomatic.Application.Tests;

internal class MockVideoImporter : IVideoImporter
{
    public Task<Video> Import(Uri uri)
    {
        var video = new Video
        {            
            ProviderId = "dQw4w9WgXcQ",
            Title = "Just three sentences",
            VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
            Description = "Just 3 sencences and a link to Rick Astley's official music video for “Never Gonna Give You Up”",
            //Transcript = transcript,
            //Thumbnails = new [] { thumbnail1, thumbnail2}
        };

        var transcriptLines = new List<TranscriptLine>();
        transcriptLines.Add(new TranscriptLine
        {
            Duration = TimeSpan.FromSeconds(2),
            StartsAt = TimeSpan.FromSeconds(0),
            Text = "First sentence..."
        });

        transcriptLines.Add(new TranscriptLine
        {
            Duration = TimeSpan.FromSeconds(2.5),
            StartsAt = TimeSpan.FromSeconds(2),
            Text = "Second sentence..."
        });

        transcriptLines.Add(new TranscriptLine
        {
            Duration = TimeSpan.FromSeconds(1.8),
            StartsAt = TimeSpan.FromSeconds(5),
            Text = "Third and final sentence"
        });

        var transcript = new Transcript 
        {
            Lines = transcriptLines
        };

        var thumbnail1 = new Thumbnail()
        {
            Resolution = ThumbnailResolution.Default,
            Url = "https://i.ytimg.com/vi/dQw4w9WgXcQ/hqdefault.jpg",
            ETag = "Wxsa",
            Height = 200,   
            Width = 200        
        };

        var thumbnail2 = new Thumbnail()
        {
            Resolution = ThumbnailResolution.MaxRes,
            Url = "https://i.ytimg.com/vi/dQw4w9WgXcQ/hqdefault.jpg",
            ETag = "Wxsa",
            Height = 500,
            Width = 500
        };


        video.Thumbnails.Add(thumbnail1);

        return Task.FromResult(video);
    }
}