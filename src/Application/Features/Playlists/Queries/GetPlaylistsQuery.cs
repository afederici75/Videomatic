namespace Application.Features.Playlists.Queries;

public class GetPlaylistsQuery(
    string? searchText = null,
    string? orderBy = null,
    int? skip = null,
    int? take = null,
    TextSearchType? searchType = null,
    // 
    IEnumerable<PlaylistId>? playlistIds = null,
    string? searchOn = null) : IRequest<Page<PlaylistDTO>>, IBasicQuery
{ 
    public string? SearchText { get; } = searchText;
    public string? OrderBy { get; } = orderBy;
    public int? Skip { get; } = skip;
    public int? Take { get; } = take;
    public TextSearchType? SearchType { get; } = searchType;
    public IEnumerable<PlaylistId>? PlaylistIds { get; } = playlistIds;
    public string? SearchOn { get; } = searchOn;    

    #region Validator

    internal class GetPlaylistsQueryValidator : AbstractValidator<GetPlaylistsQuery>
    {
        public GetPlaylistsQueryValidator()
        {
            //When(x => x.SearchText is not null, () =>
            //{
            //    RuleFor(x => x.SearchText).NotEmpty();
            //});

            When(x => x.OrderBy is not null, () =>
            {
                RuleFor(x => x.OrderBy).NotEmpty();
            });

            When(x => x.PlaylistIds is not null, () =>
            {
                RuleFor(x => x.PlaylistIds).NotEmpty();
            });

            RuleFor(x => x.Take).GreaterThan(0);
            RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
        }
    }

    #endregion
}

public abstract class BasicQueryBuilder
{
    private string? _searchText;
    private string? _orderBy;
    private int? _take;
    private int? _skip;
    private TextSearchType? _textSearchType;
    private string? _searchOn;
    private IEnumerable<PlaylistId>? _playlistIds;


    public BasicQueryBuilder()
    {
        
    }

    public BasicQueryBuilder UseSearchText(
        string searchText,
        int? skip = null,
        int? take = null,
        TextSearchType? textSearchType = TextSearchType.FreeText)
    { 
        _searchText = searchText;
        return this;
    }

    public BasicQueryBuilder UseOrderBy(string orderBy) 
    { 
        _orderBy = orderBy;
        return this;
    }

    public BasicQueryBuilder UsePagination(int take, int skip)
    { 
        _take = take;
        _skip = skip;
        return this;
    }
}

public class GetPlaylistsQueryBuilder
{
    public GetPlaylistsQueryBuilder()
    {
      
    }

    
}