namespace Company.Videomatic.Infrastructure.Data.Model;

public class VideoDb : EntityBaseDb
{
    public string Location { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string? Description { get; set; }

    public List<TagDb> Tags { get; } = new();

    public List<PlaylistDb> Playlists { get; } = new();

    public List<ArtifactDb> Artifacts { get; } = new();

    public List<ThumbnailDb> Thumbnails { get; } = new();

    public List<TranscriptDb> Transcripts { get; } = new ();
}

