namespace Company.Videomatic.Application.Handlers.Transcripts.Commands;

public class CreateTranscriptHandler : CreateAggregateRootHandler<CreateTranscriptCommand, Transcript>
{
    public CreateTranscriptHandler(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
    {
    }
}
