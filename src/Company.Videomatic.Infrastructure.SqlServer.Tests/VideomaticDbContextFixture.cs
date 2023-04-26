using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Infrastructure.SqlServer.Tests;

public class VideomaticDbContextFixture : IDisposable
{
    public VideomaticDbContextFixture(VideomaticDbContext context)
    {
        DbContext = context;
        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();
    }

    bool _skipDeletingDatabase = false;
    [Obsolete("This is a hack to check the database data if tests don't run successfully.")]
    public void SkipDeletingDatabase()
    { 
        _skipDeletingDatabase = true;
    }

    public VideomaticDbContext DbContext { get; }

    public void Dispose()
    {
        if (!_skipDeletingDatabase)
            DbContext.Database.EnsureDeleted();
    }

    int[] _videoIds; 
    public async Task<int[]> CommitAllYouTubeVideos()
    {
        if (_videoIds != null)
            return _videoIds;        
        
        // Loads all videos from the TestData folder
        string[] videoIds = YouTubeVideos.GetVideoIds();        
        Task<Video>[] tasks = videoIds
            .Select(v => VideoDataGenerator.CreateVideoFromFileAsync(v, includeAll: true))
            .ToArray();

        Video[] videos = await Task.WhenAll(tasks);
        
        // Commits all videos to the database
        DbContext.AddRange(videos);
        await DbContext.SaveChangesAsync();
        
        // Returns the updated video ids
        _videoIds = videos.Select(v => v.Id).ToArray();
        return _videoIds;
    }
}
