using Ardalis.Specification;

namespace Company.Videomatic.Domain.Specifications;

public class TranscriptByVideoIdsSpecification : Specification<Transcript>
{
    public TranscriptByVideoIdsSpecification(params VideoId[] ids)
    {
        Query.Where(v => ids.Contains(v.VideoId));
    }
}

