namespace Application.Features.Playlists.Commands;

// TODO: iffy names

public class ImportYoutubePlaylistsCommand(IEnumerable<string> urls) : IRequest<Result<IEnumerable<string>>>
{ 
    public IEnumerable<string> Urls { get; } = urls;

    internal class Validator : AbstractValidator<ImportYoutubePlaylistsCommand>
    {
        public Validator()
        {
            RuleForEach(v => v.Urls)
                .NotEmpty();
            //.Must(url => Uri.TryCreate(url, UriKind.Absolute, out _));        
        }
    }

    internal class Handler : IRequestHandler<ImportYoutubePlaylistsCommand, Result<IEnumerable<string>>>
    {
        readonly IBackgroundJobClient JobClient;

        public Handler(IBackgroundJobClient jobClient)
        {
            JobClient = jobClient ?? throw new ArgumentNullException(nameof(jobClient));
        }

        public Task<Result<IEnumerable<string>>> Handle(ImportYoutubePlaylistsCommand request, CancellationToken cancellationToken)
        {
            var jobIds = new List<string>();
            foreach (var id in request.Urls)
            {
                var jobId = JobClient.Enqueue<IVideoImporter>(imp => imp.ImportPlaylistsAsync(new[] { id }, null, cancellationToken));
                jobIds.Add(jobId);
            }

            return Task.FromResult(Result.Success<IEnumerable<string>>(jobIds));
        }
    }
}