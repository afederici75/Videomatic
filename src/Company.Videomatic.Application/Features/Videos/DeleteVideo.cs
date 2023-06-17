namespace Company.Videomatic.Application.Features.Videos;

/// <summary>
/// This command is used to delete a video from the repository.
/// </summary>
/// <param name="Id"> The id of the video to delete. </param>
public record DeleteVideoCommand(int Id) : IRequest<DeleteVideoResponse>;

/// <summary>
/// This response is returned by DeleteVideoCommand.
/// </summary>
public record DeleteVideoResponse(Video? Item, bool Deleted);

/// <summary>
/// This event is published when a video is deleted.
/// </summary>
public record VideoDeletedEvent(Video Item) : INotification;

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

/// <summary>
/// The handler for DeleteVideoCommand.    
/// Triggers <see cref="VideoDeletedEvent"/>.
/// </summary>
public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand, DeleteVideoResponse>
{
    readonly IVideoRepository _repository;
    readonly IPublisher _publisher;

    public DeleteVideoCommandHandler(IVideoRepository repository, IPublisher publisher)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    public async Task<DeleteVideoResponse> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

        //var target = await _repository.GetByIdAsync(request.Id, null, cancellationToken);
        //if (target == null)
        //    return new DeleteVideoResponse(null, false);
        //
        //await _repository.DeleteRangeAsync(new[] { target }, cancellationToken);
        //
        //await _publisher.Publish(new VideoDeletedEvent(target), cancellationToken);
        //
        //return new DeleteVideoResponse(target, true);
    }
}