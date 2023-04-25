using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Videomatic.Infrastructure.SqlServer;

public partial class VideomaticDbContext : IVideoStorage
{
    public async Task<bool> DeleteVideoAsync(int id)
    {
        var res = Videos.Remove(Video.WithId(id));
        return await SaveChangesAsync() > 1;
    }

    public Task<Video?> GetVideoByIdAsync(int id)
    {
         return Videos
            .Include(v => v.Artifacts)
            .Include(v => v.Thumbnails)
            .Include(v => v.Transcripts)
            .FirstOrDefaultAsync(v => v.Id == id);
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
}