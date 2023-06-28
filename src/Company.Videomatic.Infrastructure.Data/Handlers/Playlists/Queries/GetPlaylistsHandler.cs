//using System.Linq.Dynamic.Core;

using System.Linq;
using System.Linq.Expressions;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Queries;

public sealed class GetPlaylistsHandler : BaseRequestHandler<GetPlaylistsQuery, PageResult<PlaylistDTO>>
{
    public GetPlaylistsHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
    
    Dictionary<string, Expression<Func<Playlist, object?>>> SupportedOrderBys = new (StringComparer.OrdinalIgnoreCase)
    {
        { nameof(Playlist.Id), _ => _.Id },
        { nameof(Playlist.Name), _ => _.Name },
        { nameof(Playlist.Description), _ => _.Description },
        { "VideoCount", _ => _.Videos.Count },
    };

    public override async Task<PageResult<PlaylistDTO>> Handle(
        GetPlaylistsQuery request, CancellationToken cancellationToken = default)
    {
        IQueryable<Playlist> query = DbContext.Playlists;

        // Query setup        
        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            // Search text fields must be specified directly
            query = query.Where(x => 
                x.Name.Contains(request.SearchText) || 
                x.Description!.Contains(request.SearchText));            
        }
        
        // Custom OrderBy which takes in account what we allow to sort by
        query = query.OrderBy(request.OrderBy, SupportedOrderBys);
        
        // Mapping
        var dtoQuery = query.Select(x => new PlaylistDTO(
              x.Id,
              x.Name,
              x.Description,
              x.Videos.Count));        

        var page = await dtoQuery
            .ToPageAsync(request.Page ?? 1, request.PageSize ?? 10, cancellationToken);

        return page;
    }

    private Expression<Func<Playlist, object>> DoIt()
    {
        throw new NotImplementedException();
    }
}
