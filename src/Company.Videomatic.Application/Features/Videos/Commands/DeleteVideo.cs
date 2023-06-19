namespace Company.Videomatic.Application.Features.Videos.Commands;

/// <summary>
/// This command is used to delete a video from the repository.
/// </summary>
/// <param name="Id"> The id of the video to delete. </param>
public record DeleteVideoCommand(long Id) : IRequest<DeleteVideoResponse>;

/// <summary>
/// This response is returned by DeleteVideoCommand.
/// </summary>
public record DeleteVideoResponse(long Id, bool Deleted);

/// <summary>
/// This event is published when a video is deleted.
/// </summary>
//public record VideoDeletedEvent(Video Item) : INotification;

/// <summary>
/// The validator for DeleteVideoCommand.
/// </summary>
public class DeleteVideoCommandValidator : AbstractValidator<DeleteVideoCommand>
{
    public DeleteVideoCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}