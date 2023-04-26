using Ardalis.Specification.EntityFrameworkCore;
using Company.Videomatic.Application;
using Company.Videomatic.Application.Features.Videos.Queries.GetVideos;
using Company.Videomatic.Domain.Model;
using Company.Videomatic.Domain.Specifications;
using Microsoft.Extensions.Options;

namespace Company.Videomatic.Infrastructure.SqlServer;

public partial class VideomaticDbContext : IVideoRepository
{
    public async Task<bool> DeleteVideoAsync(int id)
    {
        var res1 = Videos.Remove(Video.WithId(id));
        var res2 = await SaveChangesAsync();
        return res2 >= 1;
    }
    
    /// <summary>
    /// Smart updates the video. If the video has an id, it will be updated. Otherwise it will be added.
    /// </summary>
    /// <param name="video">The video to smart update.</param>
    /// <returns>The id of the video that was updated or inserted.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<int> UpdateVideoAsync(Video video)
    {
        if (video is null)
        {
            throw new ArgumentNullException(nameof(video));
        }

        if (video.Id == 0)
        {
            Videos.Add(video);
        }
        else
        {
            Videos.Update(video);
        }

        var res = await SaveChangesAsync();
        return video.Id;
    }

    public async Task<Video> GetVideoByIdAsync(GetVideoSpecification spec)
    {
        Video res = await Videos.WithSpecification(spec)
            .FirstAsync();
        return res;
    }

    public async Task<IEnumerable<Video>> GetVideosAsync(GetVideosSpecification spec)
    {
        var res = await Videos.WithSpecification(spec)
            .ToListAsync();

        return res;
    }
}