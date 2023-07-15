namespace Company.Videomatic.Application.Handlers.Transcripts.Queries;

public class GetTranscriptHandler :
    IRequestHandler<GetTranscriptsQuery, PageResult<TranscriptDTO>>
{
    public GetTranscriptHandler(IReadRepository<Transcript> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    readonly IReadRepository<Transcript> _repository;
    readonly IMapper _mapper;

    public async Task<PageResult<TranscriptDTO>> Handle(GetTranscriptsQuery request, CancellationToken cancellationToken)
    {
        var spec = new TranscriptsFilteredAndPaginated(
            request.SearchText,
            request.Page,
            request.PageSize,
            request.OrderBy);

        var res = await _repository.PageAsync(
            spec,
            vid => _mapper.Map<TranscriptDTO>(vid),
            spec.Page,
            spec.PageSize,
            cancellationToken);

        return res;
    }
}