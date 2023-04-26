namespace Company.Videomatic.Domain.Specifications;

public class GetVideosSpec : Specification<Video>
{
    public GetVideosSpec(
        string? title = default, 
        string? description = default,
        string? providerId = default,
        int? skip = 0,
        int? take = 10)
    {
        if (!string.IsNullOrWhiteSpace(title))
        {
            Query.Where(x => (x.Title != null) && (x.Title.StartsWith(title)));
        }
        
        if (!string.IsNullOrWhiteSpace(description)) 
        {
            Query.Where(x => (x.Description != null) && (x.Description.StartsWith(description)));
        }

        if (!string.IsNullOrWhiteSpace(providerId))
        {
            Query.Where(x => (x.ProviderId != null) && (x.ProviderId.StartsWith(providerId)));
        }

        if (skip.HasValue)
        {
            Query.Skip(skip.Value);
        }
        if (take.HasValue)
        {
            Query.Take(take.Value);
        }        
    }
}

