namespace Infrastructure.Data.Configurations.Converters;

public class ArtifactIdConverter : ValueConverter<ArtifactId, int>, IStronglyTypedIdConverter
{
    public ArtifactIdConverter() : base(id => id.Value, value => new ArtifactId(value))
    { }
}
