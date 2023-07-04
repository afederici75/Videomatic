namespace Company.Videomatic.Infrastructure.Data.Handlers.Transcripts.Queries;

public class GetTranscriptHandler :
    IRequestHandler<GetTranscriptsQuery, PageResult<TranscriptDTO>>,
    IRequestHandler<GetTranscriptsByIdQuery, IEnumerable<TranscriptDTO>>
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

    public async Task<IEnumerable<TranscriptDTO>> Handle(GetTranscriptsByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new TranscriptByIdsSpecification(
            request.TranscriptIds.Select(x => new TranscriptId(x)));

        var videos = await _repository.ListAsync(spec, cancellationToken);

        return videos.Select(vid => _mapper.Map<TranscriptDTO>(vid));
    }
}