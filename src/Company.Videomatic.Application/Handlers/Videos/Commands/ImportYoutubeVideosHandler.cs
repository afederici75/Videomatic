using Hangfire;
using MediatR;

namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public sealed class ImportYoutubeVideosHandler : IRequestHandler<ImportYoutubeVideosCommand, ImportYoutubeVideosResponse>
{
    public ImportYoutubeVideosHandler(
        IBackgroundJobClient jobClient)
    {
        JobClient = jobClient ?? throw new ArgumentNullException(nameof(jobClient));        
    }

    public IBackgroundJobClient JobClient { get; }


    public Task<ImportYoutubeVideosResponse> Handle(ImportYoutubeVideosCommand request, CancellationToken cancellationToken = default)
    {
        var jobIds = new List<string>();
        foreach (var url in request.Urls)
        {
            var jobId = JobClient.Enqueue<IVideoImporter>(imp => 
                imp.ImportVideosAsync(new[] { url }, request.DestinationPlaylistId ?? 1, null, cancellationToken));

            jobIds.Add(jobId);
        }

        return Task.FromResult(new ImportYoutubeVideosResponse(true, jobIds, request.DestinationPlaylistId));
    }   
}