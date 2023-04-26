namespace Company.Videomatic.Domain.Specifications;

public class GetVideoByProviderVideoIdSpec : Specification<Video>, ISingleResultSpecification<Video>
{
    public GetVideoByProviderVideoIdSpec(string videoProviderId)
    {
        Query.Where(x => x.ProviderVideoId == videoProviderId);
    }
}

