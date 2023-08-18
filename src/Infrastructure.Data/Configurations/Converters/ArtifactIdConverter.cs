namespace Infrastructure.Data.Configurations.Converters;

[StronglyTypedIdConverter]
public class ArtifactIdConverter : ValueConverter<ArtifactId, int>
{
    public ArtifactIdConverter() : base(id => id.Value, value => new ArtifactId(value))
    { }
}
