using MediatR;

namespace Company.Videomatic.Domain.Specifications;

public class GetVideoSpecification : Specification<Video>, 
    IRequest<Video>,
    ISingleResultSpecification<Video>
{
    public GetVideoSpecification(int id)
    {
        Query.Where(x => x.Id == id);
    }

    public GetVideoSpecification(string? videoProviderId, string? videoUrl)
    {        
        Query.Where(x =>         
         ((videoProviderId == null) || x.ProviderVideoId.StartsWith(videoProviderId))
         &&
         ((videoUrl == null) || x.VideoUrl.StartsWith(videoUrl))
         );
    }

    public GetVideoSpecification(string providerVideoId)
    {
        Query.Where(x => x.ProviderVideoId == providerVideoId);
    }
}

