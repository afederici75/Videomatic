using Domain.Videos;

namespace Application.Specifications;

public class VideosByProviderItemIdSpec : Specification<Video>
{
    public VideosByProviderItemIdSpec(string providerId, IEnumerable<string> itemIds)
    {
        Query.Where(v =>
            v.Origin.ProviderId.Equals(providerId) &&
            itemIds.Contains(v.Origin.ProviderItemId)
        );
    }
}
