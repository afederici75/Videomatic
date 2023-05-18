namespace VideomaticBlazor.Data;

public class VideoCardModel
{
    public int Id { get; set; }
    
    /// <summary>
    /// The name of the video.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The length of the video.
    /// </summary>
    public TimeSpan Duraction { get; set; }

    /// <summary>
    /// The description of the video.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The URL of the thumbnail to display.
    /// </summary>
    public string? ThumnbnailUrl { get; set; }

    /// <summary>
    /// The date the video was published.
    /// </summary>
    public DateTime PublishedOn { get; set; }

    /// <summary>
    /// The date the video was imported.
    /// </summary>
    public DateTime ImportedOn { get; set; }
}