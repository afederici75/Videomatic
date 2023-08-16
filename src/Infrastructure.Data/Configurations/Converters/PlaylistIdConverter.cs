using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Configurations.Converters;

public class PlaylistIdConverter : ValueConverter<PlaylistId, int>, IStronglyTypedIdConverter
{
    public PlaylistIdConverter()
        : base(id => id.Value,
               value => new PlaylistId(value))
    { }
}
