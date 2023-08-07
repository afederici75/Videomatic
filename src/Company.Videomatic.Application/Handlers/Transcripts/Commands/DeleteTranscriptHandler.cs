using AutoMapper;
using Company.SharedKernel.Abstractions;
using Company.SharedKernel.Common.CQRS;

namespace Company.Videomatic.Application.Handlers.Transcripts.Commands;

public class DeleteTranscriptHandler : DeleteEntityHandler<DeleteTranscriptCommand, Transcript, TranscriptId>
{
    public DeleteTranscriptHandler(IRepository<Transcript> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    protected override TranscriptId ConvertIdOfRequest(DeleteTranscriptCommand request) => request.Id;
}
