namespace Application.Specifications;

public static class Videos
{
    public class ByIds : Specification<Video>
    {
        public ByIds(params VideoId[] videoIds)
        {
            Query.Where(v => videoIds.Contains(v.Id));
        }
    }

    //public class ByPlaylistIds : Specification<Video>
    //{
    //    public ByPlaylistIds(IEnumerable<PlaylistId> playlistIds)
    //    {
    //        Query.Where(v =>
    //            v.Origin.ProviderId.Equals(providerId) &&
    //            providerItemIds.Contains(v.Origin.ProviderItemId)
    //        );
    //    }
    //}

    public class ByProviderItemId : Specification<Video>
    {
        public ByProviderItemId(string providerId, IEnumerable<string> providerItemIds)
        {
            Query.Where(v =>
                v.Origin.ProviderId.Equals(providerId) &&
                providerItemIds.Contains(v.Origin.ProviderItemId)
            );
        }
    }
}