namespace Company.Videomatic.Application.Handlers.Transcripts.Commands;

public class DeleteTranscriptHandler : DeleteAggregateRootHandler<DeleteTranscriptCommand, Transcript, TranscriptId>
{
    public DeleteTranscriptHandler(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
    {
    }

    protected override TranscriptId ConvertIdOfRequest(DeleteTranscriptCommand request) => request.Id;
}
