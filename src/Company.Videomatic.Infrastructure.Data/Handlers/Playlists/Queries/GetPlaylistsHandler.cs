using Company.Videomatic.Infrastructure.Data.Extensions;
//using System.Linq.Dynamic.Core;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Queries;

public sealed class GetPlaylistsHandler : BaseRequestHandler<GetPlaylistsQuery, PageResult<PlaylistDTO>>
{
    public GetPlaylistsHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    private static readonly Dictionary<string, IOrderBy> OrderFunctions =
    new Dictionary<string, IOrderBy>(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(PlaylistDTO.Id), new OrderBy<PlaylistDTO, long>(x => x.Id) },
        { nameof(PlaylistDTO.Name),  new OrderBy<PlaylistDTO, string>(x => x.Name) },
        { nameof(PlaylistDTO.Description),   new OrderBy<PlaylistDTO, string?>(x => x.Description) },
        { nameof(PlaylistDTO.VideoCount),   new OrderBy<PlaylistDTO, long?>(x => x.VideoCount) },

    };

    public override async Task<PageResult<PlaylistDTO>> Handle(
        GetPlaylistsQuery request, CancellationToken cancellationToken = default)
    {
        var query = from pl in DbContext.Playlists
                    //select new PlaylistDTO(
                    //    pl.Id,
                    //    pl.Name,
                    //    pl.Description,
                    //    0);// pl.PlaylistVideos.Count);
                    select new
                    {
                        pl.Id,
                        pl.Name,
                        pl.Description,
                        VideoCount = pl.PlaylistVideos.Count
                    };

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {            
            query = query.Where(x => x.Name.Contains(request.SearchText) || x.Description!.Contains(request.SearchText));
        }
        
        if (!string.IsNullOrWhiteSpace(request.OrderBy))
        {
            var x = query.OrderBy(x => x.Id);
        }


        var test = await query.ToListAsync();

        IQueryable<PlaylistDTO> queriable = query.Select(row => new PlaylistDTO(
              row.Id,
              row.Name,
              row.Description,
              row.VideoCount));

        var page = await queriable
            .ToPageAsync(request.Page ?? 1, request.PageSize ?? 10, cancellationToken);

        return page;
    }

}
