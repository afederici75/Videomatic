namespace Company.Videomatic.Domain.Aggregates.Artifact;

public record ArtifactId(long Value = 0) : ILongId
{
    public static implicit operator long(ArtifactId x) => x.Value;
    public static implicit operator ArtifactId(long x) => new (x);
}

