namespace Application.Features.Playlists.Commands;

// TODO: iffy names

public partial record ImportYoutubePlaylistsCommand(IEnumerable<string> Urls) : IRequest<ImportYoutubePlaylistsResponse>;

public record ImportYoutubePlaylistsResponse(bool Queued, IEnumerable<string> JobIds);

//public record VideoImportedEvent(int VideoId, int ThumbNailCount, int TranscriptCount, int ArtifactsCount) : INotification;

internal class ImportYoutubePlaylistCommandValidator : AbstractValidator<ImportYoutubePlaylistsCommand>
{
    public ImportYoutubePlaylistCommandValidator()
    {
        RuleForEach(v => v.Urls)
            .NotEmpty();
            //.Must(url => Uri.TryCreate(url, UriKind.Absolute, out _));        
    }
}