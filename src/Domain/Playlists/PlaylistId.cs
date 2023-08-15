namespace Domain.Playlists;

public record PlaylistId(int Value = 0)
{    
    public static implicit operator int(PlaylistId x) => x.Value;
    public static implicit operator PlaylistId(int x) => new (x);
}