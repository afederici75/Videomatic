namespace VideomaticBlazor.Data;

public class VideoCardModel
{
    /// <summary>
    /// The unique id of the video.
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// The name of the video.
    /// </summary>
    public string? Name { get; set; }

    public string? Source { get; set; }

    /// <summary>
    /// The length of the video.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// The description of the video.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The URL of the thumbnail to display.
    /// </summary>
    public string? Thumnbnail { get; set; }

    /// <summary>
    /// The date the video was published.
    /// </summary>
    public DateTime PublishedOn { get; set; }

    /// <summary>
    /// The date the video was imported.
    /// </summary>
    public DateTime ImportedOn { get; set; }
}