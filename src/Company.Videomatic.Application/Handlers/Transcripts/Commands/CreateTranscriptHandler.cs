using Company.SharedKernel.Common.CQRS;

namespace Company.Videomatic.Application.Handlers.Transcripts.Commands;

public class CreateTranscriptHandler : CreateEntitytHandler<CreateTranscriptCommand, Transcript>
{
    public CreateTranscriptHandler(IRepository<Transcript> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
