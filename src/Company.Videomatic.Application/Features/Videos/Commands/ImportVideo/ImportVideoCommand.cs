using Company.Videomatic.Application.Abstractions;
using MediatR;

namespace Company.Videomatic.Application.Features.Videos.Commands.ImportVideo;

public class ImportVideoCommand : IRequest<ImportVideoResponse>
{
    public required string VideoUrl { get; set; }

    public class ImportVideoLinkHandler : IRequestHandler<ImportVideoCommand, ImportVideoResponse>
    {
        readonly IVideoImporter _importer;
        readonly IVideoStorage _storage;
        readonly IVideoAnalyzer _analyzer;

        public ImportVideoLinkHandler(
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
            var video = await _importer.ImportAsync(new Uri(request.VideoUrl));
            
            var summary = _analyzer.SummarizeVideoAsync(video);
            var review = _analyzer.ReviewVideoAsync(video);

            var videoId = await _storage.UpdateVideoAsync(video);

            return new ImportVideoResponse { VideoId = videoId };
        }

    }
}
