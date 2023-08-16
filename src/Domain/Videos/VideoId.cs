namespace Domain.Videos;

public readonly record struct VideoId(int Value = 0)
{
    public static implicit operator int(VideoId x) => x.Value;
    public static explicit operator VideoId(int x) => new(x);
}
