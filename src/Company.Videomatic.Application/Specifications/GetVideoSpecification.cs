namespace Company.Videomatic.Application.Specifications;

public class GetVideoSpecification : GetOneSpecification<Video>,
    IRequest<Video>
{
    public GetVideoSpecification(
        int id,
        string[]? includes = null)
        : base(id, includes)
    { }

    public GetVideoSpecification(
        string? providerVideoId = default, 
        string? videoUrl = default, 
        string[]? includes = default)    
        : base(includes)
    {
        if (!string.IsNullOrWhiteSpace(providerVideoId))
        {
            Query.Where(x => x.ProviderVideoId.StartsWith(providerVideoId));
        }

        if (!string.IsNullOrWhiteSpace(videoUrl))
        {
            Query.Where(x => (x.VideoUrl.StartsWith(videoUrl)));
        }
    }
}
