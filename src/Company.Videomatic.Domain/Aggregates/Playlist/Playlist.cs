namespace Company.Videomatic.Domain.Aggregates.Playlist;

public class Playlist : IAggregateRoot
{
    public static Playlist Create(string name, string? description)
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

    public void AddVideo(VideoId videoId)
    {
        _playlistVideos.Add(PlaylistVideo.Create(Id, videoId));
    }

    #region Private

    private List<PlaylistVideo> _playlistVideos = new List<PlaylistVideo>();

    private Playlist() { }

    #endregion
}