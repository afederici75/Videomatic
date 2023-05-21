namespace Company.Videomatic.Application.Features.Videos;

/// <summary>
/// This command is used to delete a video from the repository.
/// </summary>
/// <param name="VideoId"> The id of the video to delete. </param>
public record DeleteVideoCommand(int VideoId) : IRequest<DeleteVideoResponse>;

/// <summary>
/// This response is returned by DeleteVideoCommand.
/// </summary>
public record DeleteVideoResponse(Video? Video, bool Deleted);

/// <summary>
/// This event is published when a video is deleted.
/// </summary>
public record VideoDeletedEvent(Video Video) : INotification;

/// <summary>
/// The validator for DeleteVideoCommand.
/// </summary>
public class DeleteVideoCommandValidator : AbstractValidator<DeleteVideoCommand>
{
    public DeleteVideoCommandValidator()
    {
        RuleFor(x => x.VideoId).GreaterThan(0);
    }
}

/// <summary>
/// The handler for DeleteVideoCommand.    
/// Triggers <see cref="VideoDeletedEvent"/>.
/// </summary>
public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand, DeleteVideoResponse>
{
    readonly IRepository<Video> _repository;
    readonly IPublisher _publisher;

    public DeleteVideoCommandHandler(IRepository<Video> repository, IPublisher publisher)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    public async Task<DeleteVideoResponse> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
    {
        var target = await _repository.GetByIdAsync(request.VideoId, null, cancellationToken);
        if (target == null)
            return new DeleteVideoResponse(null, false);

        await _repository.DeleteRangeAsync(new[] { target }, cancellationToken);

        await _publisher.Publish(new VideoDeletedEvent(target), cancellationToken);

        return new DeleteVideoResponse(target, true);
    }
}