namespace Company.Videomatic.Application.Features.Videos.Commands.DeleteVideo;


/// <summary>
/// This command is used to delete a video from the repository.
/// </summary>
/// <param name="VideoId"> The id of the video to delete. </param>
public record DeleteVideoCommand(int VideoId) : IRequest<DeleteVideoResponse>;