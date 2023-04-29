namespace Company.Videomatic.Application.Features.Videos.ImportVideo;

/// <summary>
/// This command is used to import video data located at a given url.
/// </summary>
public partial class ImportVideoCommand : IRequest<ImportVideoResponse>
{
    public ImportVideoCommand(string videoUrl)
    {
        if (string.IsNullOrEmpty(videoUrl))
        {
            throw new ArgumentException($"'{nameof(videoUrl)}' cannot be null or empty.", nameof(videoUrl));
        }

        VideoUrl = videoUrl;
    }

    public string VideoUrl { get; init; }
}
