using Company.Videomatic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Videomatic.Application.Abstractions;

public class VideoQueryOptions
{
    public static VideoQueryOptions Default { get; } = new VideoQueryOptions();

    public static VideoQueryOptions IncludeAll { get; } = new VideoQueryOptions()
    { 
        IncludeArtifacts = true,
        IncludeThumbnails = true,
        IncludeTranscripts = true
    };

    public bool IncludeArtifacts { get; set; } = false;
    public bool IncludeThumbnails { get; set; } = false;
    public bool IncludeTranscripts { get; set; } = false;
}

public interface IVideoStorage
{
    Task<int> UpdateVideoAsync(Video video);
    Task<bool> DeleteVideoAsync(int id);
    Task<Video?> GetVideoByIdAsync(int id, VideoQueryOptions? options = null);
}