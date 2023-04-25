﻿namespace Company.Videomatic.Application.Tests.Mocks;

/// <summary>
/// This class implements the IVideoStorage interface and stores the videos in memory.
/// </summary>
internal class InMemoryVideoStorage : IVideoStorage
{
    private readonly Dictionary<int, Video> _videos = new();
    public Task<int> UpdateVideoAsync(Video video)
    {
        if (video.Id <= 0)
        {
            video.SetId(_videos.Count + 1);
        }
        _videos[video.Id] = video;
        return Task.FromResult(video.Id);
    }

    public Task<bool> DeleteVideoAsync(int id)
    {
        return Task.FromResult(_videos.Remove(id));
    }

    public Task<Video?> GetVideoByIdAsync(int id, VideoQueryOptions? options = null)
    {
        options ??= VideoQueryOptions.Default;
        if (_videos.TryGetValue(id, out var video))
        {
            if (!options.IncludeTranscripts)
            {
                video.ClearTranscripts();
            }

            if (!options.IncludeThumbnails)
            {
                video.ClearThumbnails();
            }

            if (!options.IncludeArtifacts)
            {
                video.ClearArtifacts();
            }

            return Task.FromResult<Video?>(video);
        }
        return Task.FromResult<Video?>(null);
    }
}