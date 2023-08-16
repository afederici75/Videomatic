namespace Application.Features.Videos.Commands;

public readonly record struct ImportYoutubeVideosCommand(IEnumerable<string> Urls, int? DestinationPlaylistId = null) : IRequest<ImportYoutubeVideosResponse>;

public readonly record struct ImportYoutubeVideosResponse(bool Queued, IEnumerable<string> JobIds, int? PlaylistId = null);

//public readonly record struct VideoImportedEvent(int VideoId, int ThumbNailCount, int TranscriptCount, int ArtifactsCount) : INotification;

internal class ImportYoutubeVideoCommandValidator : AbstractValidator<ImportYoutubeVideosCommand>
{
    public ImportYoutubeVideoCommandValidator()
    {
        RuleForEach(v => v.Urls)
            .NotEmpty();
            //.Must(url => Uri.TryCreate(url, UriKind.Absolute, out _));        
    }
}