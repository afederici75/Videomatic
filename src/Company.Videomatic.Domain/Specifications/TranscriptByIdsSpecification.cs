using Ardalis.Specification;
using Company.Videomatic.Domain.Aggregates.Transcript;

namespace Company.Videomatic.Domain.Specifications;

public class TranscriptByIdsSpecification : Specification<Transcript>
{
    public TranscriptByIdsSpecification(params TranscriptId[] ids)
    {
        Query.Where(v => ids.Contains(v.Id));
    }
}

