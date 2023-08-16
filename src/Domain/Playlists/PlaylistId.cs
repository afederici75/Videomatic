using Domain.Videos;

namespace Domain.Playlists;

// https://www.ssw.com.au/rules/do-you-use-strongly-typed-ids/
public readonly record struct PlaylistId(int Value)
{
    public static implicit operator int(PlaylistId x) => x.Value;
    public static explicit operator PlaylistId(int x) => new(x);
}