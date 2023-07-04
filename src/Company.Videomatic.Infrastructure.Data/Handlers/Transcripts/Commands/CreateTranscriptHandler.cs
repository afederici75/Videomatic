namespace Company.Videomatic.Infrastructure.Data.Handlers.Transcripts.Commands;

public class CreateTranscriptHandler : CreateEntityHandlerBase<CreateTranscriptCommand, CreateTranscriptResponse, Transcript>
{
    public CreateTranscriptHandler(IRepository<Transcript> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    protected override CreateTranscriptResponse CreateResponseFor(Transcript createdEntity)    
        => new CreateTranscriptResponse(Id: createdEntity.Id);
}
