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
