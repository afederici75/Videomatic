namespace Company.Videomatic.Domain.Queries;

public class GetVideosQuery : GetManySpecification<Video>,
    IRequest<Video>
{
    public GetVideosQuery(int[] ids, string[]? orderBy = default)
        : base(ids, orderBy)
    { 
        Query.Where(x => ids.Contains(x.Id));
    }

    public GetVideosQuery(
        int take = 10,        
        string? titlePrefix = default, 
        string? descriptionPrefix = default,
        string? providerIdPrefix = default,
        
        int? skip = 0,
        string[]? includes = null, 
        string[]? orderBy = default)

        : base(take: take, skip: skip, includes: includes, orderBy: orderBy)
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
    }
}