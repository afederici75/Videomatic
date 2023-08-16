namespace Application.Features.Videos.Commands;

// TODO: iffy name
public class ImportYoutubeVideosCommand(IEnumerable<string> urls, PlaylistId? destinationPlaylistId = null) : IRequest<Result<string>>
{
    public IEnumerable<string> Urls { get; } = urls;
    public PlaylistId? DestinationPlaylistId { get; } = destinationPlaylistId;


    internal class Validator : AbstractValidator<ImportYoutubeVideosCommand>
    {
        public Validator()
        {
            RuleForEach(v => v.Urls)
                .NotEmpty();
            //.Must(url => Uri.TryCreate(url, UriKind.Absolute, out _));        
        }
    }


    internal class Handler : IRequestHandler<ImportYoutubeVideosCommand, Result<string>>
    {
        public Handler(
            IBackgroundJobClient jobClient)
        {
            JobClient = jobClient ?? throw new ArgumentNullException(nameof(jobClient));
        }

        public IBackgroundJobClient JobClient { get; }


        public Task<Result<string>> Handle(ImportYoutubeVideosCommand request, CancellationToken cancellationToken = default)
        {
            var jobId = JobClient.Enqueue<IVideoImporter>(imp => imp.ImportVideosAsync(request.Urls, (PlaylistId?)request.DestinationPlaylistId, null, cancellationToken));
            
            return Task.FromResult(new Result<string>(jobId));
        }
    }
}