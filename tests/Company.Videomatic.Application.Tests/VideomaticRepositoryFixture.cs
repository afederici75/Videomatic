namespace Company.Videomatic.Application.Tests;

public class VideomaticRepositoryFixture : IAsyncLifetime
{
    public VideomaticRepositoryFixture(IRepositoryBase<Video> repository)
        : base()
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    protected bool SkipInsertTestData { get; set; }
    [Obsolete("This is a hack to check the database data if tests don't run successfully.")]
    public bool SkipDeletingDatabase { get; set; }

    public IRepositoryBase<Video> Repository { get; }

    public virtual Task DisposeAsync() => Task.CompletedTask;

    public async Task InitializeAsync()
    {
        if (SkipInsertTestData)
            return;
        
        // Loads all videos from the TestData folder
        string[] videoIds = YouTubeVideos.GetVideoIds();
        Task<Video>[] tasks = videoIds
            .Select(v => VideoDataGenerator.CreateVideoFromFileAsync(v, includeAll: true))
            .ToArray();

        Video[] videos = await Task.WhenAll(tasks);
        await Repository.AddRangeAsync(videos);
    }
}
