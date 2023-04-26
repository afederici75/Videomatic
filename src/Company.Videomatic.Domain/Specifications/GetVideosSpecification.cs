 namespace Company.Videomatic.Domain.Specifications;

public class GetVideosSpecification : Specification<Video>
{
    public GetVideosSpecification(params int[] ids)
    { 
        Query.Where(x => ids.Contains(x.Id));
    }

    public GetVideosSpecification(
        string? titlePrefix = default, 
        string? descriptionPrefix = default,
        string? providerIdPrefix = default,
        int? skip = 0,
        int? take = 10)
    {
        if (!string.IsNullOrWhiteSpace(titlePrefix))
        {
            Query.Where(x => (x.Title != null) && (x.Title.StartsWith(titlePrefix)));
        }
        
        if (!string.IsNullOrWhiteSpace(descriptionPrefix)) 
        {
            Query.Where(x => (x.Description != null) && (x.Description.StartsWith(descriptionPrefix)));
        }

        if (!string.IsNullOrWhiteSpace(providerIdPrefix))
        {
            Query.Where(x => (x.ProviderId != null) && (x.ProviderId.StartsWith(providerIdPrefix)));
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

