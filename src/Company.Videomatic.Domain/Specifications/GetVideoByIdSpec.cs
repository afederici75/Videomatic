namespace Company.Videomatic.Domain.Specifications;

public class GetVideoByIdSpec : Specification<Video>, ISingleResultSpecification<Video>
{
    public GetVideoByIdSpec(int id)
    {
        Query.Where(x => x.Id == id);
    }
}

