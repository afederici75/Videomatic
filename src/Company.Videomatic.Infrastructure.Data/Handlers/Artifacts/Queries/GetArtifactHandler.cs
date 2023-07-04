namespace Company.Videomatic.Infrastructure.Data.Handlers.Artifacts.Queries;

public class GetArtifactHandler : 
    IRequestHandler<GetArtifactsQuery, PageResult<ArtifactDTO>>,
    IRequestHandler<GetArtifactsByIdQuery, IEnumerable<ArtifactDTO>>
{
    public GetArtifactHandler(IReadRepository<Artifact> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    readonly IReadRepository<Artifact> _repository;
    readonly IMapper _mapper;
    
    public async Task<PageResult<ArtifactDTO>> Handle(GetArtifactsQuery request, CancellationToken cancellationToken)
    {
        var spec = new ArtifactsFilteredAndPaginated(
            request.SearchText,
            request.Page,
            request.PageSize,
            request.OrderBy);

        var res = await _repository.PageAsync<Artifact, ArtifactDTO>(
            spec,
            vid => _mapper.Map<ArtifactDTO>(vid),
            spec.Page,
            spec.PageSize,
            cancellationToken);

        return res;
    }

    public async Task<IEnumerable<ArtifactDTO>> Handle(GetArtifactsByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new ArtifactsByIdsSpecification(
            request.ArtifactIds.Select(x => new ArtifactId(x)));

        var videos = await _repository.ListAsync(spec, cancellationToken);

        return videos.Select(vid => _mapper.Map<ArtifactDTO>(vid));
    }
}