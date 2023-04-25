namespace Company.Videomatic.Application.Features.Videos.Commands.UpdateVideo;

/// <summary>
/// This command is used to update a video in the repository.
/// </summary>
public partial class UpdateVideoCommand : IRequest<UpdateVideoResponse>
{
    public UpdateVideoCommand(int videoId, string? title = default, string? description = default)
    {
        VideoId = videoId;
        Title = title;
        Description = description;
    }

    /// <summary>
    /// The id of the video to update.
    /// </summary>
    public int VideoId { get; init; }
    /// <summary>
    /// The new title of the video. (null to leave unchanged)
    /// </summary>
    public string? Title { get; init; }
    /// <summary>
    /// The new description of the video. (null to leave unchanged)
    /// </summary>
    public string? Description { get; init; }    
}