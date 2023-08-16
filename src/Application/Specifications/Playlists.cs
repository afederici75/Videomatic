namespace Application.Specifications;

public static class Playlists
{
    public class WithVideos : Specification<Playlist>
    {
        public WithVideos(params PlaylistId[] playlistIds)
        {
            Query.Where(pl => playlistIds.Contains(pl.Id))
                 .Include(pl => pl.Videos);
        }
    }

    public class ByIds : Specification<Playlist>
    {
        public ByIds(IEnumerable<PlaylistId> playlistIds)
        {
            Query.Where(v => playlistIds.Contains(v.Id));
        }

        public ByIds(PlaylistId playlistId)
            : this(new[] { playlistId }) 
        { }
    }

    public class ByProviderAndItemId : Specification<Playlist>
    {
        public ByProviderAndItemId(string providerId, IEnumerable<string> itemIds)
        {
            Query.Where(v =>
                v.Origin.ProviderId.Equals(providerId) &&
                itemIds.Contains(v.Origin.ProviderItemId)
            );
        }
    }
}