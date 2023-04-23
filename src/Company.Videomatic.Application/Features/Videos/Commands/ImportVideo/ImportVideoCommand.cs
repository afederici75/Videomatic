using Company.Videomatic.Application.Abstractions;
using MediatR;
using System.Net;

namespace Company.Videomatic.Application.Features.Videos.Commands.ImportVideo;

public class ImportVideoCommand : IRequest<ImportVideoResponse>
{
    public required string VideoUrl { get; set; }

    public class ImportVideoLinkHandler : IRequestHandler<ImportVideoCommand, ImportVideoResponse>
    {
        readonly IVideoImporter _importer;
        readonly IVideoStorage _storage;

        public ImportVideoLinkHandler(IVideoImporter importer, IVideoStorage storage)
        {
            _importer = importer;
            this._storage = storage;
        }

        public async Task<ImportVideoResponse> Handle(ImportVideoCommand request, CancellationToken cancellationToken)
        {
            var video = await _importer.Import(new Uri(request.VideoUrl));
            var videoId = await _storage.UpdateVideo(video);

            return new ImportVideoResponse { VideoId = videoId };
        }

    }
}
