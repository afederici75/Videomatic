namespace Company.Videomatic.Domain.Specifications.Artifacts;

public class ArtifactsByIdsSpecification : Specification<Artifact>
{
    public ArtifactsByIdsSpecification(IEnumerable<ArtifactId> ids)
    {
        Query.Where(a => ids.Contains(a.Id));
    }
}

