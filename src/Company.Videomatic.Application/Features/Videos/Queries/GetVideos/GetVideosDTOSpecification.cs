using Company.SharedKernel.Specifications;

namespace Company.Videomatic.Application.Features.Videos.Queries.GetVideos;

public class GetVideosDTOSpecification : GetManySpecification<VideoDTO>
{
    public GetVideosDTOSpecification(int[] ids, string[]? orderBy = default)
       : base(ids, orderBy)
    { }

    public GetVideosDTOSpecification(
        int take = 10,

        string? titlePrefix = default,
        string? descriptionPrefix = default,
        string? providerIdPrefix = default,

        int? skip = 0,
        string[]? includes = null,
        string[]? orderBy = default)
        : base(take, skip, includes, orderBy)
    { }
}
