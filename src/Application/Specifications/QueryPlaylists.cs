namespace Application.Specifications;

public static class QueryPlaylists
{
    [Flags]
    public enum Include
    { 
        None = 0,
        Videos = 1,
    }

    public class ById : Specification<Playlist>, ISingleResultSpecification<Playlist>
    {
        public ById(IEnumerable<PlaylistId> playlistIds, Include include = default)
        {
            Query.Where(pl => playlistIds.Contains(pl.Id))
                 .ApplyIncludes(include);
        }

        public ById(PlaylistId playlistId, Include include = default)
        {
            Query.Where(pl => playlistId == pl.Id)
                 .ApplyIncludes(include);
        }
    }    

    public class ByProviderAndItemId : Specification<Playlist>
    {
        public ByProviderAndItemId(string providerId, IEnumerable<string> itemIds, Include include = default)
        {
            Query.Where(v =>v.Origin.ProviderId.Equals(providerId) && itemIds.Contains(v.Origin.ProviderItemId))
                 .ApplyIncludes(include); 
        }
    }

    static ISpecificationBuilder<Playlist> ApplyIncludes(
        this ISpecificationBuilder<Playlist> query,
        Include include)
    {
        if (include.HasFlag(Include.Videos))
        {
            query.Include(v => v.Videos);
        }

        return query;
    }
}