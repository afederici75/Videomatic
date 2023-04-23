using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Videomatic.Drivers.SqlServer;

public partial class VideomaticDbContext : IVideoStorage
{
    public async Task<bool> DeleteVideo(int id)
    {
        var res = Videos.Remove(Video.WithId(id));
        return await SaveChangesAsync() > 1;
    }

    public async Task<int> UpdateVideo(Video video)
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