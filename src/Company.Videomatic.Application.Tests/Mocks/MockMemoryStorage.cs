namespace Company.Videomatic.Application.Tests.Mocks;

/// <summary>
/// This class implements the IVideoStorage interface and stores the videos in memory.
/// </summary>
internal class MockVideoStorage : IVideoStorage
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

    public async Task<Video?> GetVideoByIdAsync(int id, params string includes = null)
    {
        var query = await GetVideosAsync(
            filter: (v) => v.Id == id,
            paging: (v) => v.Take(1));

        var video = query.FirstOrDefault();
        return video;
        //options ??= VideoQueryOptions.Default;
        //if (_videos.TryGetValue(id, out var video))
        //{
        //    if (!options.IncludeTranscripts)
        //    {
        //        video.ClearTranscripts();
        //    }
        //
        //    if (!options.IncludeThumbnails)
        //    {
        //        video.ClearThumbnails();
        //    }
        //
        //    if (!options.IncludeArtifacts)
        //    {
        //        video.ClearArtifacts();
        //    }
        //
        //    return Task.FromResult<Video?>(video);
        //}
        //return Task.FromResult<Video?>(null);
    }

    public Task<Video[]> GetVideosAsync(
        Func<Video, bool>? filter = null, 
        Func<IQueryable<Video>, IQueryable<Video>>? sort = null, 
        Func<IQueryable<Video>, IQueryable<Video>>? paging = null, 
        CancellationToken cancellationToken = default)
    {
        var vq = _videos.Values.AsQueryable();

        if (filter is not null)
            vq = vq.Where(filter).AsQueryable(); 

        if (sort is not null)   
              vq = sort(vq);

        if (paging is not null)
            vq = paging(vq);
        
        return Task.FromResult<Video[]>(vq.ToArray());
    }
}