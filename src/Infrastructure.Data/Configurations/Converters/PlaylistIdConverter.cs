namespace Infrastructure.Data.Configurations.Converters;

[StronglyTypedIdConverter]
public class PlaylistIdConverter : ValueConverter<PlaylistId, int>
{
    public PlaylistIdConverter()
        : base(id => id.Value,
               value => new PlaylistId(value))
    { }
}
