using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain;

namespace Company.Videomatic.Drivers.GoogleDrive;

public class MockGoogleDriveVideoProvider : IVideoProvider
{
    public string Name => "Google Drive";

    public Task<IEnumerable<Folder>> GetRoot()
    {
        var videosFolder = new Folder
        {
            Name = "Videos",
            Children = new List<Folder>
            {
                new Folder
                {
                    Name = "Travel",
                    Videos = new List<VideoLink>
                    {
                        new VideoLink
                        {
                            ProviderId = "1",
                            Title = "Trip to Thailand",
                            Description = "A travelogue of a trip to Thailand.",
                            VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                            ThumbnailUrl = "https://img.youtube.com/vi/dQw4w9WgXcQ/maxresdefault.jpg"
                        },
                        new VideoLink
                        {
                            ProviderId = "2",
                            Title = "Exploring Japan",
                            Description = "A tour of Japan's most beautiful sights.",
                            VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                            ThumbnailUrl = "https://img.youtube.com/vi/dQw4w9WgXcQ/maxresdefault.jpg"
                        }
                    }
                },
                new Folder
                {
                    Name = "Festivals",
                    Children = new List<Folder>
                    {
                        new Folder
                        {
                            Name = "Italy",
                            Videos = new List<VideoLink>
                            {
                                new VideoLink
                                {
                                    ProviderId = "3",
                                    Title = "Carnivale in Venice",
                                    Description = "Experience the beauty of Venice during Carnivale.",
                                    VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                                    ThumbnailUrl = "https://img.youtube.com/vi/dQw4w9WgXcQ/maxresdefault.jpg"
                                },
                                new VideoLink
                                {
                                    ProviderId = "4",
                                    Title = "Tuscan Wine Festival",
                                    Description = "Join us as we taste the best wines of Tuscany.",
                                    VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                                    ThumbnailUrl = "https://img.youtube.com/vi/dQw4w9WgXcQ/maxresdefault.jpg"
                                }
                            }
                        },
                        new Folder
                        {
                            Name = "Burning Man",
                            Videos = new List<VideoLink>
                            {
                                new VideoLink
                                {
                                    ProviderId = "5",
                                    Title = "Burning Man Documentary",
                                    Description = "Go behind the scenes of the world's biggest art festival.",
                                    VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                                    ThumbnailUrl = "https://img.youtube.com/vi/dQw4w9WgXcQ/maxresdefault.jpg"
                                }
                            }
                        }
                    }
                }
            }
        };

        var moviesFolder = new Folder
        {
            Name = "Movies",
            Children = new List<Folder>
            {
                new Folder
                {
                    Name = "Trip to Italy",
                    Videos = new List<VideoLink>
                    {
                        new VideoLink
                        {
                            ProviderId = "6",
                            Title = "A Trip to Italy",
                            Description = "A romantic comedy about two friends who go to Italy.",
                            VideoUrl = "https://img.youtube.com/vi/dQw4w9WgXcQ/maxresdefault.jpg"
                        }
                    }
                },
                new Folder
                {
                    Name = "Van Life",
                    Videos = new List<VideoLink>
                    {
                        new VideoLink
                        {
                            ProviderId = "7",
                            Title = "Life on the Road",
                            Description = "Join us on our journey through America's national parks.",
                            VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                            ThumbnailUrl = "https://img.youtube.com/vi/dQw4w9WgXcQ/maxresdefault.jpg"
                        }
                    }
                }
            }
        };

        var result = new List<Folder> { videosFolder, moviesFolder };
        return Task.FromResult<IEnumerable<Folder>>(result);
    }        
}