namespace Company.Videomatic.Domain.Aggregates.Playlist;

public interface IIntegerId
{
    int GetId();
}

public record PlaylistId(int Value = 0) : IIntegerId
{
    public int GetId() => this;

    public static implicit operator int(PlaylistId x) => x.Value;
    public static implicit operator PlaylistId(int x) => new (x);
}