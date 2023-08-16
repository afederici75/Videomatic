namespace Application.Features.Videos.Commands;

// TODO: iffy name
public class ImportYoutubeVideosCommand(IEnumerable<string> urls, int? destinationPlaylistId = null) : IRequest<Result<string>>
{
    public IEnumerable<string> Urls { get; } = urls;
    public int? DestinationPlaylistId { get; } = destinationPlaylistId;


    internal class ImportYoutubeVideoCommandValidator : AbstractValidator<ImportYoutubeVideosCommand>
    {
        public ImportYoutubeVideoCommandValidator()
        {
            RuleForEach(v => v.Urls)
                .NotEmpty();
            //.Must(url => Uri.TryCreate(url, UriKind.Absolute, out _));        
        }
    }
}