using Company.Videomatic.Application.Features.Transcript.Commands;
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
        var spec = new TranscriptByVideoIdsSpecification(request.VideoId);

        var Transcript = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (Transcript is null)
        {
            Transcript = await _repository.AddAsync(_mapper.Map<Transcript>(request));
        }
        else
        {
            _mapper.Map(request, Transcript);
        }

        var cnt = await _repository.SaveChangesAsync(cancellationToken);

        return new CreateTranscriptResponse(Transcript.Id);
    }
}