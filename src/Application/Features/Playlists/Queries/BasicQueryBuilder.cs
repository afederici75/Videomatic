namespace Application.Features.Playlists.Queries;

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
