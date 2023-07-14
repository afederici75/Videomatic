namespace Company.Videomatic.Domain.Aggregates.Video;

public record VideoId(long Value = 0) : ILongId
{
    public static implicit operator long(VideoId x) => x.Value;
    public static implicit operator VideoId(long x) => new (x);
}
