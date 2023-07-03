using Company.Videomatic.Application.Features.Artifacts.Queries;
using Company.Videomatic.Application.Features.Playlists;
using Company.Videomatic.Application.Features.Transcripts;
using Company.Videomatic.Domain.Aggregates.Transcript;
using Company.Videomatic.Domain.Specifications;
using System.Linq.Expressions;
using Company.Videomatic.Domain.Extensions;
using Company.Videomatic.Domain.Aggregates.Artifact;
using Company.Videomatic.Domain.Specifications.Transcripts;

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

    Dictionary<string, Expression<Func<Transcript, object?>>> SupportedOrderBys = new(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(Transcript.Id), _ => _.Id },
        { nameof(Transcript.Language), _ => _.Language },
        //{ nameof(Transcript.Description), _ => _.Description },
        //{ "VideoCount", _ => _.PlaylistVideos.Count },
    };

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