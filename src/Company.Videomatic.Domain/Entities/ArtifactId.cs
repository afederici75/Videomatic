namespace Company.Videomatic.Domain.Entities;

public record ArtifactId(long Value = 0)
{
    public static implicit operator long(ArtifactId x) => x.Value;
    public static implicit operator ArtifactId(long x) => new ArtifactId(x);
}

