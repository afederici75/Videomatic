namespace Company.Videomatic.Application.Features.Collections;

public record ImportCollectionCommand(string CollectionUrl) : IRequest<ImportCollectionResponse>;

public record ImportCollectionResponse(bool found);

public class ImportCollectionHandler : IRequestHandler<ImportCollectionCommand, ImportCollectionResponse>
{
    private readonly IRepository<VideoCollection> _collectionRepository;
    private readonly IPlaylistImporter _importer;
    private readonly IPublisher _publisher;

    public ImportCollectionHandler(IRepository<VideoCollection> collectionRepository, IPlaylistImporter importer, IPublisher publisher)
    {
        _collectionRepository = collectionRepository ?? throw new ArgumentNullException(nameof(collectionRepository));
        _importer = importer ?? throw new ArgumentNullException(nameof(_importer));
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }
    public async Task<ImportCollectionResponse> Handle(ImportCollectionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //Collection newCollection = await _importer.ImportAsync(new Uri(request.CollectionUrl));
        //
        //await _collectionRepository.AddRangeAsync(new[] { newCollection }, cancellationToken);
        //
        //var response = new ImportCollectionResponse(true);

        //return response;
    }
}