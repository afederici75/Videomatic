using Company.Videomatic.Application.Abstractions;
using MediatR;

namespace Company.Videomatic.Application.Features.Videos.Commands.ImportVideoLink;

public class ImportVideoLinkHandler : IRequestHandler<ImportVideoLinkCommand, ImportVideoLinkResponse>
{
    readonly IVideoImporter _importer;

    public ImportVideoLinkHandler(IVideoImporter importer)
    {
        _importer = importer;
    }
    
    public async Task<ImportVideoLinkResponse> Handle(ImportVideoLinkCommand request, CancellationToken cancellationToken)
    {
        var video = await _importer.Import(new Uri(request.VideoUrl));
        return new ImportVideoLinkResponse { VideoLinkId = 0 };
    }

}