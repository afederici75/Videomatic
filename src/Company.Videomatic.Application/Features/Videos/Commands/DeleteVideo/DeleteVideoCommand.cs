namespace Company.Videomatic.Application.Features.Videos.Commands.DeleteVideo;

/// <summary>
/// This command is used to delete a video from the repository.
/// </summary>
public partial class DeleteVideoCommand : IRequest<DeleteVideoResponse>
{
    public DeleteVideoCommand(int videoId)
    {
        VideoId = videoId;
    }

    /// <summary>
    /// The id of the video to delete.
    /// </summary>
    public int VideoId { get; init; }    
}