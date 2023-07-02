namespace Company.Videomatic.Domain.Entities.VideoAggregate;

public record VideoId(long Value = 0)
{
    public static implicit operator long(VideoId x) => x.Value;
    public static implicit operator VideoId(long x) => new VideoId(x);
}
