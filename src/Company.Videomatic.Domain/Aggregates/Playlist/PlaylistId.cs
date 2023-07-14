namespace Company.Videomatic.Domain.Aggregates.Playlist;

public record PlaylistId(long Value = 0) : ILongId
{
    public static implicit operator long(PlaylistId x) => x.Value;
    public static implicit operator PlaylistId(long x) => new (x);
}
