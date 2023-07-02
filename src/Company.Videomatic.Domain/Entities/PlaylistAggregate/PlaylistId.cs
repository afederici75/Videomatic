namespace Company.Videomatic.Domain.Entities.PlaylistAggregate;

public record PlaylistId(long Value = 0)
{
    public static implicit operator long(PlaylistId x) => x.Value;
    public static implicit operator PlaylistId(long x) => new PlaylistId(x);
}
