using Ardalis.Specification;

namespace Company.Videomatic.Domain.Specifications;

public class TranscriptByIdsSpecification : Specification<Transcript>
{
    public TranscriptByIdsSpecification(params TranscriptId[] ids)
    {
        Query.Where(v => ids.Contains(v.Id));
    }
}

