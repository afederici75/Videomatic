using Company.Videomatic.Application.Features.Playlists;
using Company.Videomatic.Domain.Extensions;
using Company.Videomatic.Domain.Specifications;
using Company.Videomatic.Domain.Specifications.Playlists;
using System.Linq.Expressions;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Queries;

public sealed class GetPlaylistsHandler : 
    IRequestHandler<GetPlaylistsQuery, PageResult<PlaylistDTO>>,
    IRequestHandler<GetPlaylistsByIdQuery, IEnumerable<PlaylistDTO>>
{
    public GetPlaylistsHandler(IReadRepository<Playlist> repository, IMapper mapper) 
    {
        _repository = repository;
        _mapper = mapper;
    }

    readonly IReadRepository<Playlist> _repository;
    readonly IMapper _mapper;
    
    public async Task<PageResult<PlaylistDTO>> Handle(GetPlaylistsQuery request, CancellationToken cancellationToken)
    {
        var spec = new PlaylistsFilteredAndPaginated(
            request.SearchText,
            request.Page,
            request.PageSize,
            request.OrderBy);

        var res = await _repository.PageAsync<Playlist, PlaylistDTO>(
            spec,
            vid => _mapper.Map<PlaylistDTO>(vid),
            spec.Page,
            spec.PageSize,
            cancellationToken);

        return res;
    }

    public async Task<IEnumerable<PlaylistDTO>> Handle(GetPlaylistsByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new PlaylistsByIdSpecification(request.PlaylistIds.Select(x => new PlaylistId(x)));

        var videos = await _repository.ListAsync(spec, cancellationToken);

        return videos.Select(vid => _mapper.Map<PlaylistDTO>(vid));

    }
}