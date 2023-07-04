using Ardalis.Specification;
using Company.Videomatic.Domain.Aggregates.Transcript;

namespace Company.Videomatic.Domain.Specifications.Transcripts;

public class TranscriptByIdsSpecification : Specification<Transcript>
{
    public TranscriptByIdsSpecification(IEnumerable<TranscriptId> ids)
    {
        Query.Where(v => ids.Contains(v.Id));
    }
}

