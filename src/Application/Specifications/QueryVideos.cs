namespace Application.Specifications;

public static class QueryVideos
{
    public class ByIds : Specification<Video>
    {
        public ByIds(IEnumerable<VideoId> videoIds)
        {
            Query.Where(v => videoIds.Contains(v.Id));
        }        
    }

    public class ById : Specification<Video>, ISingleResultSpecification<Video>
    {
        public ById(VideoId videoId)
        {
            Query.Where(v => videoId == v.Id);
        }
    }

    public class ByProviderItemIds : Specification<Video>
    {
        public ByProviderItemIds(string providerId, IEnumerable<string> providerItemIds)
        {
            Query.Where(v =>
                v.Origin.ProviderId.Equals(providerId) &&
                providerItemIds.Contains(v.Origin.ProviderItemId)
            );
        }
    }

    public class ByProviderItemId: Specification<Video>, ISingleResultSpecification<Video>
    {
        public ByProviderItemId(string providerId, string providerItemId)
        {
            Query.Where(v =>
                v.Origin.ProviderId.Equals(providerId) && providerItemId.Equals(v.Origin.ProviderItemId)
            );
        }
    }
}