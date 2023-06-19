namespace Company.Videomatic.Infrastructure.Data.Model;

public class Video : EntityBase
{
    public string Location { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string? Description { get; set; }

    public List<VideoTag> VideoTags { get; } = new();

    public List<Playlist> Playlists { get; } = new();
    public List<PlaylistVideo> PlaylistVideos { get; } = new();

    public List<Artifact> Artifacts { get; } = new();

    public List<Thumbnail> Thumbnails { get; } = new();

    public List<Transcript> Transcripts { get; } = new ();
}
