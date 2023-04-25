using System.Diagnostics;

namespace Company.Videomatic.Application.Features.Videos.Commands.ImportVideo;

public partial class ImportVideoCommand
{
    /// <summary>
    /// The handler for ImportVideoCommand.
    /// </summary>
    public class ImportVideoCommandHandler : IRequestHandler<ImportVideoCommand, ImportVideoResponse>
    {
        readonly IVideoImporter _importer;
        readonly IVideoStorage _storage;
        readonly IVideoAnalyzer _analyzer;

        public ImportVideoCommandHandler(
            IVideoImporter importer,
            IVideoStorage storage,
            IVideoAnalyzer analyzer)
        {
            _importer = importer ?? throw new ArgumentNullException(nameof(importer));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _analyzer = analyzer;
        }

        public async Task<ImportVideoResponse> Handle(ImportVideoCommand request, CancellationToken cancellationToken)
        {
            // Imports the video   
            Video video = await _importer.ImportAsync(new Uri(request.VideoUrl));

            // Generates artifacts for the video
            Task<Artifact> summaryTask = _analyzer.SummarizeVideoAsync(video);
            Task<Artifact> reviewTask = _analyzer.ReviewVideoAsync(video);
            
            Artifact[] artifacts = await Task.WhenAll(summaryTask, reviewTask);

            video.AddArtifacts(artifacts);

            // Creates the video 
            var videoId = await _storage.UpdateVideoAsync(video);

            return new ImportVideoResponse(video);
        }

    }
}