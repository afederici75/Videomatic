using Company.Videomatic.Application.Features.Transcripts.Commands;
using Company.Videomatic.Domain.Aggregates.Transcript;
using Company.Videomatic.Domain.Specifications.Transcripts;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Transcripts.Commands;

public class CreateTranscriptHandler : IRequestHandler<CreateTranscriptCommand, CreateTranscriptResponse>
{
    private readonly IRepository<Transcript> _repository;
    private readonly IMapper _mapper;

    public CreateTranscriptHandler(IRepository<Transcript> repository,
                                   IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CreateTranscriptResponse> Handle(CreateTranscriptCommand request, CancellationToken cancellationToken)
    {
        //Transcript newTranscript = _mapper.Map<CreateTranscriptCommand, Transcript>(request); // TODO: Does not work with Lines

        Transcript newTranscript = Transcript.Create(request.VideoId, request.Language, request.Lines); 

        var entry = await _repository.AddAsync(newTranscript);        

        return new CreateTranscriptResponse(newTranscript.Id);
    }
}
