namespace Company.Videomatic.Application.Features.Videos.Commands;

/// <summary>
/// This command is used to import video data located at a given url.
/// </summary>
public partial record ImportVideoCommand(string Location, int? PlaylistId) : IRequest<ImportVideoResponse>;

/// <summary>
/// The response returned by ImportVideoCommand.
/// </summary>
/// <param name="VideoId"></param>
public record ImportVideoResponse(bool Queued, int VideoId, int? PlaylistId = null);

/// <summary>
/// This event is published when a video is imported.
/// </summary>
/// <param name="VideoId"></param>
/// <param name="ThumbNailCount"></param>
/// <param name="TranscriptCount"></param>
/// <param name="ArtifactsCount"></param>
//public record VideoImportedEvent(int VideoId, int ThumbNailCount, int TranscriptCount, int ArtifactsCount) : INotification;

/// <summary>
/// The validator for ImportVideoCommand.
/// </summary>
internal class ImportVideoCommandValidator : AbstractValidator<ImportVideoCommand>
{
    public ImportVideoCommandValidator()
    {
        RuleFor(v => v.Location).NotEmpty();
    }
}