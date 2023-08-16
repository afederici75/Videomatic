namespace Application.Features.Playlists.Commands;

// TODO: iffy names

public class ImportYoutubePlaylistsCommand(IEnumerable<string> Urls) : IRequest<Result<IEnumerable<string>>>
{ 
    public IEnumerable<string> Urls { get; } = Urls;
    internal class ImportYoutubePlaylistCommandValidator : AbstractValidator<ImportYoutubePlaylistsCommand>
    {
        public ImportYoutubePlaylistCommandValidator()
        {
            RuleForEach(v => v.Urls)
                .NotEmpty();
            //.Must(url => Uri.TryCreate(url, UriKind.Absolute, out _));        
        }
    }
}