using MediatR;
using System.Net;

namespace Company.Videomatic.Application.Features.Videos.Commands.ImportVideoLink;

public class ImportVideoLinkCommand : IRequest<ImportVideoLinkResponse>
{
    public required string VideoUrl { get; set; }   
}
