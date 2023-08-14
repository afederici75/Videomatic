namespace Application.Specifications;

public class VideosByIds : Specification<Video>
{
    public VideosByIds(params VideoId[] videoIds)
    {
        Query.Where(v => videoIds.Contains(v.Id));
    }
}

public class VideosByProviderItemId : Specification<Video>
{
    public VideosByProviderItemId(string providerId, IEnumerable<string> providerItemIds)
    {
        Query.Where(v =>
            v.Origin.ProviderId.Equals(providerId) &&
            providerItemIds.Contains(v.Origin.ProviderItemId)
        );
    }
}
