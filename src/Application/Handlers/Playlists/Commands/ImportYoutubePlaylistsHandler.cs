namespace Application.Handlers.Playlists.Commands;

public class ImportYoutubePlaylistsHandler : IRequestHandler<ImportYoutubePlaylistsCommand, ImportYoutubePlaylistsResponse>
{
    readonly IBackgroundJobClient JobClient;

    public ImportYoutubePlaylistsHandler(
        IBackgroundJobClient jobClient)
    {
        JobClient = jobClient ?? throw new ArgumentNullException(nameof(jobClient));        
    }

    public Task<ImportYoutubePlaylistsResponse> Handle(ImportYoutubePlaylistsCommand request, CancellationToken cancellationToken)
    {
        var jobIds = new List<string>();
        foreach (var id in request.Urls)
        {
            var jobId = JobClient.Enqueue<IVideoImporter>(imp => imp.ImportPlaylistsAsync(new[] { id }, null, cancellationToken));
            jobIds.Add(jobId);
        }        

        return Task.FromResult(new ImportYoutubePlaylistsResponse(true, jobIds));
    }
}