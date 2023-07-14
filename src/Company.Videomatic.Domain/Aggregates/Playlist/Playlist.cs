namespace Company.Videomatic.Domain.Aggregates.Playlist;

public class Playlist : IAggregateRoot<PlaylistId>
{
    public static Playlist Create(string name, string? description = null)
    {
        return new Playlist
        {
            Name = name,
            Description = description
        };
    }

    public PlaylistId Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }

    #region Private

    private Playlist() { }

    #endregion
}