using Company.Videomatic.Application.Features.Videos;
using System.ComponentModel.DataAnnotations;

namespace Company.Videomatic.Application.Features.Collections;

/// <summary>
/// Queues the import of videos into an existing collection.
/// </summary>
/// <param name="VideoUrls"></param>
public record AddVideosToCollectionCommand(int CollectionId, string[] VideoUrls) : IRequest<AddVideosToCollectionResponse>;

/// <summary>
/// The response to AddVideosToCollectionCommand.
/// </summary>
/// <param name="CollectionId">The collection that is being updated.</param>
/// <param name="VideoCount">The number of scheduled videos to be added to the collection.</param>
public record AddVideosToCollectionResponse(int CollectionId, int VideoCount);

/// <summary>
/// Validates the AddVideosToCollectionCommand.
/// </summary>
public class AddVideosToCollectionCommandValidator : AbstractValidator<AddVideosToCollectionCommand>
{
    public AddVideosToCollectionCommandValidator()
    {
        RuleFor(x => x.CollectionId).GreaterThan(0);
        RuleFor(x => x.VideoUrls).NotEmpty();
    }
}

/// <summary>
/// Handles the AddVideosToCollectionCommand.
/// </summary>
public class AddVideosToCollectionHandler : IRequestHandler<AddVideosToCollectionCommand, AddVideosToCollectionResponse>
{
    private readonly IRepository<Collection> _repository;
    private readonly IVideoImporter _importer;
    private readonly IPublisher _publisher;

    public AddVideosToCollectionHandler(IRepository<Collection> collectionRepository, IVideoImporter importer, IPublisher publisher)
    {
        _repository = collectionRepository ?? throw new ArgumentNullException(nameof(collectionRepository));
        _importer = importer ?? throw new ArgumentNullException(nameof(importer));
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    public async Task<AddVideosToCollectionResponse> Handle(AddVideosToCollectionCommand request, CancellationToken cancellationToken)
    {
        var collection = await _repository.GetByIdAsync(request.CollectionId, new[] { nameof(Collection.Videos) }, cancellationToken);
        if (collection == null)
            throw new NotFoundException(request.CollectionId.ToString(), nameof(Collection));

        var commands = request.VideoUrls.Select(url => _publisher.Publish(new ImportVideoCommand(request.CollectionId, url)));
        await Task.WhenAll(commands);

        return new AddVideosToCollectionResponse(request.CollectionId, commands.Count());
    }
}