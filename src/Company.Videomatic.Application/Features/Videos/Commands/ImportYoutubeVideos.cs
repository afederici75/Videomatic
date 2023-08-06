namespace Company.Videomatic.Application.Features.Videos.Commands;

public partial record ImportYoutubeVideosCommand(IEnumerable<string> Urls, int? DestinationPlaylistId = null) : IRequest<ImportYoutubeVideosResponse>;

public record ImportYoutubeVideosResponse(bool Queued, IEnumerable<string> JobIds, int? PlaylistId = null);

//public record VideoImportedEvent(int VideoId, int ThumbNailCount, int TranscriptCount, int ArtifactsCount) : INotification;

internal class ImportYoutubeVideoCommandValidator : AbstractValidator<ImportYoutubeVideosCommand>
{
    public ImportYoutubeVideoCommandValidator()
    {
        RuleForEach(v => v.Urls)
            .NotEmpty();
            //.Must(url => Uri.TryCreate(url, UriKind.Absolute, out _));        
    }
}