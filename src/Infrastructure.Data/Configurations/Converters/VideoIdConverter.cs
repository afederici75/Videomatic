namespace Infrastructure.Data.Configurations.Converters;

[StronglyTypedIdConverter]
public class VideoIdConverter : ValueConverter<VideoId, int>
{
    public VideoIdConverter()
        : base(
            id => id.Value,
            value => new VideoId(value)
        )
    { }
}
