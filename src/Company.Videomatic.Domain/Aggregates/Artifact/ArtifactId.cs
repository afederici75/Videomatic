namespace Company.Videomatic.Domain.Aggregates.Artifact;

public record ArtifactId(int Value = 0)
{
    public static implicit operator int(ArtifactId x) => x.Value;
    public static implicit operator ArtifactId(int x) => new (x);
}