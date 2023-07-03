using Ardalis.Specification;
using Company.Videomatic.Domain.Aggregates.Artifact;

namespace Company.Videomatic.Domain.Specifications;

public class ArtifactByVideoIdsSpecification : Specification<Artifact>
{
    public ArtifactByVideoIdsSpecification(params VideoId[] ids)
    {
        Query.Where(v => ids.Contains(v.VideoId));
    }
}

