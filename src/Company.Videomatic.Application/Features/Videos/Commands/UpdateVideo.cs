namespace Company.Videomatic.Application.Features.Videos.Commands;

/// <summary>
/// This command is used to update a video in the repository.
/// </summary>
public record UpdateVideoCommand(
    int Id, 
    string Title, 
    string? Description = default) : IRequest<UpdateVideoResponse>;

/// <summary>
/// The response from the UpdateVideoCommand.
/// </summary>
/// <param name="Video"></param>
/// <param name="Updated"></param>
public record UpdateVideoResponse(long Id, bool Updated);

/// <summary>
/// This event is published when a video is updated.
/// </summary>
/// <param name="VideoId"></param>
public record VideoUpdatedEvent(int VideoId);