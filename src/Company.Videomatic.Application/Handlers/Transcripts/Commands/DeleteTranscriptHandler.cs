using Company.SharedKernel.CQRS.Commands;

namespace Company.Videomatic.Application.Handlers.Transcripts.Commands;

public class DeleteTranscriptHandler : DeleteEntityHandler<DeleteTranscriptCommand, Transcript, TranscriptId>
{
    public DeleteTranscriptHandler(IRepository<Transcript> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}