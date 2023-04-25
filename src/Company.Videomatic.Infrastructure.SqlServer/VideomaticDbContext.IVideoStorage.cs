namespace Company.Videomatic.Infrastructure.SqlServer;

public partial class VideomaticDbContext : IVideoStorage
{
    public IQueryable<Video> GetVideos() => Videos.AsNoTracking().AsQueryable();

    public async Task<bool> DeleteVideoAsync(int id)
    {
        var res = Videos.Remove(Video.WithId(id));
        return await SaveChangesAsync() > 1;
    }

    public async Task<Video?> GetVideoByIdAsync(int id, VideoQueryOptions? options = null)
    {
        options ??= new VideoQueryOptions();
        
        var query = Videos;
        if (options.IncludeTranscripts)
            query.Include( query => query.Transcripts);

        if (options.IncludeArtifacts)
            query.Include( query => query.Artifacts);

        if (options.IncludeThumbnails)
            query.Include( query => query.Thumbnails);

        var result = await query.FirstOrDefaultAsync(v => v.Id == id);
        return result;
    }

    public async Task<int> UpdateVideoAsync(Video video)
    {
        if (video is null)
        {
            throw new ArgumentNullException(nameof(video));
        }

        if (video.Id == 0)
        {
            await Videos.AddAsync(video);
        }
        else
        {
            Videos.Update(video);
        }

        return await SaveChangesAsync();
    }

    public Task<Video[]> GetVideosAsync(
        Func<IQueryable<Video>, IQueryable<Video>>? filter = null, 
        Func<IQueryable<Video>, IQueryable<Video>>? sort = null, 
        Func<IQueryable<Video>, IQueryable<Video>>? paging = null, 
        CancellationToken cancellationToken = default)
    {
        var query = Videos.AsQueryable();

        if (filter is not null)
            query = filter(query);

        if (sort is not null)
            query = sort(query);

        if (paging is not null)
           query = paging(query);

        return query.ToArrayAsync(cancellationToken);
    }
}