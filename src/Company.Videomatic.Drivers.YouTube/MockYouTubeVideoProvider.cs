using Company.Videomatic.Domain;
using Company.Videomatic.Drivers.YouTube.Options;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Videomatic.Application.Abstractions;

public class MockYouTubeVideoProvider : IVideoProvider
{
    public MockYouTubeVideoProvider(IOptions<YouTubeOptions> options)
    {
        Options = options.Value;
    }
    public string Name => "YouTube";

    public YouTubeOptions Options { get; }

    public Task<IEnumerable<Folder>> GetRoot()
    {
        var developmentFolder = new Folder
        {
            Name = "Development",
            Videos = new List<Video>
            {
                new Video
                {
                    Id = "1",
                    Title = "Introduction to C#",
                    Description = "A beginner's guide to C# programming language.",
                    VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                    ThumbnailUrl = "https://img.youtube.com/vi/dQw4w9WgXcQ/maxresdefault.jpg"
                },
                new Video
                {
                    Id = "2",
                    Title = "Advanced Python",
                    Description = "Learn how to write advanced Python code.",
                    VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                    ThumbnailUrl = "https://img.youtube.com/vi/dQw4w9WgXcQ/maxresdefault.jpg"
                }
            }
        };

        var diyFolder = new Folder
        {
            Name = "DIY",
            Videos = new List<Video>
            {
                new Video
                {
                    Id = "3",
                    Title = "How to build a birdhouse",
                    Description = "A step-by-step guide to building a birdhouse.",
                    VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                    ThumbnailUrl = "https://img.youtube.com/vi/dQw4w9WgXcQ/maxresdefault.jpg"
                },
                new Video
                {
                    Id = "4",
                    Title = "Furniture Restoration",
                    Description = "Learn how to restore old furniture.",
                    VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                    ThumbnailUrl = "https://img.youtube.com/vi/dQw4w9WgXcQ/maxresdefault.jpg"
                },
                new Video
                {
                    Id = "5",
                    Title = "Gardening Tips",
                    Description = "Learn how to create and maintain a beautiful garden.",
                    VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                    ThumbnailUrl = "https://img.youtube.com/vi/dQw4w9WgXcQ/maxresdefault.jpg"
                }
            }
        };

        var easternPhilosophyFolder = new Folder
        {
            Name = "Eastern Philosophy",
            Videos = new List<Video>
            {
                new Video
                {
                    Id = "6",
                    Title = "Introduction to Zen Buddhism",
                    Description = "Learn the basics of Zen Buddhism.",
                    VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                    ThumbnailUrl = "https://img.youtube.com/vi/dQw4w9WgXcQ/maxresdefault.jpg"
                },
                new Video
                {
                    Id = "7",
                    Title = "Meditation Techniques",
                    Description = "Learn different meditation techniques.",
                    VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                    ThumbnailUrl = "https://img.youtube.com/vi/dQw4w9WgXcQ/maxresdefault.jpg"
            }
        }
        };

        var westernPhilosophyFolder = new Folder
        {
            Name = "Western Philosophy",
            Videos = new List<Video>
        {
            new Video
            {
                Id = "8",
                Title = "Introduction to Aristotle",
                Description = "Learn about the philosophy of Aristotle.",
                VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                ThumbnailUrl = "https://img.youtube.com/vi/dQw4w9WgXcQ/maxresdefault.jpg"
            },
            new Video
            {
                Id = "9",
                Title = "Philosophy of Science",
                Description = "Explore the relationship between philosophy and science.",
                VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                ThumbnailUrl = "https://img.youtube.com/vi/dQw4w9WgXcQ/maxresdefault.jpg"
            }
        }
        };
        
        return Task.FromResult<IEnumerable<Folder>>(new [] 
        { 
            developmentFolder, 
            diyFolder, 
            easternPhilosophyFolder, 
            westernPhilosophyFolder 
        });
    }
}
