using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Configurations.Converters;

public class VideoIdConverter : ValueConverter<VideoId, int>, IStronglyTypedIdConverter
{
    public VideoIdConverter()
        : base(
            id => id.Value,
            value => new VideoId(value)
        )
    { }
}
