using static Application.Specifications.Playlists;

namespace Application.Specifications;

public static class PlaylistSpecificationExtensions
{
    public static ISpecificationBuilder<Playlist> ApplyIncludes(
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

public static class Playlists
{
    [Flags]
    public enum Include
    { 
        None = 0,
        Videos = 1,
    }

    public class WithVideos : Specification<Playlist>
    {
        public WithVideos(params PlaylistId[] playlistIds)
        {
            Query.Where(pl => playlistIds.Contains(pl.Id))
                 .ApplyIncludes(Include.Videos);
        }
    }

    public class ById : Specification<Playlist>, ISingleResultSpecification<Playlist>
    {
        public ById(PlaylistId playlistId, Include include = Include.None)
        {
            Query.Where(v => playlistId == v.Id)
                 .ApplyIncludes(include);
                       
        }        
    }

    public class ByIds : Specification<Playlist>
    {
        public ByIds(IEnumerable<PlaylistId> playlistIds, Include include = Include.None)
        {
            Query.Where(v => playlistIds.Contains(v.Id))
                 .ApplyIncludes(include);
        }
    }

    public class ByProviderAndItemId : Specification<Playlist>
    {
        public ByProviderAndItemId(string providerId, IEnumerable<string> itemIds, Include include = Include.None)
        {
            Query.Where(v =>v.Origin.ProviderId.Equals(providerId) && itemIds.Contains(v.Origin.ProviderItemId))
                 .ApplyIncludes(include); 
        }
    }
}