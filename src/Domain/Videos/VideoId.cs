namespace Domain.Videos;

public record VideoId(int Value = 0)
{
    public static implicit operator int(VideoId x) => x.Value;
    public static implicit operator VideoId(int x) => new(x);
}
