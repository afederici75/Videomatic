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

        public ImportVideoLinkHandler(IVideoImporter importer)
        {
            _importer = importer;
        }

        public async Task<ImportVideoResponse> Handle(ImportVideoCommand request, CancellationToken cancellationToken)
        {
            var video = await _importer.Import(new Uri(request.VideoUrl));
            return new ImportVideoResponse { VideoLinkId = 0 };
        }

    }
}
