namespace Company.Videomatic.Application.Handlers.Transcripts.Commands;

public class DeleteTranscriptHandler : DeleteAggregateRootHandler<DeleteTranscriptCommand, Transcript>
{
    public DeleteTranscriptHandler(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
    {
    }
}
