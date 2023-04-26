using Company.Videomatic.Application;
using Company.Videomatic.Application.Features.Videos.Queries.GetVideos;
using Microsoft.Extensions.Options;

namespace Company.Videomatic.Infrastructure.SqlServer;

public partial class VideomaticDbContext : IVideoStorage
{
    public async Task<bool> DeleteVideoAsync(int id)
    {
        var res = Videos.Remove(Video.WithId(id));
        return await SaveChangesAsync() > 1;
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

    public async Task<Video?> GetVideoByIdAsync(int id)
    {
        var query = Videos;
        //if (options.IncludeTranscripts)
        //    query.Include(query => query.Transcripts);
        //
        //if (options.IncludeArtifacts)
        //    query.Include(query => query.Artifacts);
        //
        //if (options.IncludeThumbnails)
        //    query.Include(query => query.Thumbnails);

        var result = await query.FirstOrDefaultAsync(v => v.Id == id);
        return result;
    }

    public async Task<Video[]> GetVideosAsync(IQuerySettings settings)
    {
        Video[] results = await Videos
            .AsNoTracking()
            .ApplySettings(settings)
            .ToArrayAsync();

        return results;
    }


}