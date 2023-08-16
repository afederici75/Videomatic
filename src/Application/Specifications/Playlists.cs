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
        public ByIds(params PlaylistId[] playlistIds)
        {
            Query.Where(v => playlistIds.Contains(v.Id));
        }
    }

    public class ByProviderItemId : Specification<Playlist>
    {
        public ByProviderItemId(string providerId, IEnumerable<string> itemIds)
        {
            Query.Where(v =>
                v.Origin.ProviderId.Equals(providerId) &&
                itemIds.Contains(v.Origin.ProviderItemId)
            );
        }
    }
}