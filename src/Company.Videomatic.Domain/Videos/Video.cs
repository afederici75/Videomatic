namespace Company.Videomatic.Domain.Videos;

public record VideoId(long Value = 0)
{
    public static implicit operator long(VideoId x) => x.Value;
    public static implicit operator VideoId(long x) => new VideoId(x);
}

public class Video //: EntityBase
{
    public static Video Create(string location, string title, VideoDetails details, string? description)
    {
        return new Video
        {
            Location = location,
            Name = title,
            Description = description,
            Details = details ?? VideoDetails.CreateEmpty()
        };
    }

    public VideoId Id { get; private set; } = default!;
    public string Location { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }

    public VideoDetails Details { get; private set; } = default!;    

    public IReadOnlyCollection<VideoTag> VideoTags => _videoTags.ToList();
    public IReadOnlyCollection<Playlist> Playlists => _playLists.ToList();    
    public IReadOnlyCollection<PlaylistVideo> PlaylistVideos => _playlistVideos.ToList();
    public IReadOnlyCollection<Artifact> Artifacts => _artifacts.ToList();
    public IReadOnlyCollection<Thumbnail> Thumbnails => _thumbnails.ToList();
    public IReadOnlyCollection<Transcript> Transcripts => _transcripts.ToList();

    public VideoTag AddTag(string name)
    {
        var newTag = VideoTag.Create(Id, name);
        _videoTags.Add(newTag);
        return newTag;
    }
    
    public Thumbnail AddThumbnail(string location, ThumbnailResolution resolution, int height, int width)
    {
        var thumbnail = Thumbnail.Create(location, resolution, height, width);
        _thumbnails.Add(thumbnail);
        return thumbnail;
    }

    public Artifact AddArtifact(string title, string type, string? text = null)
    {
        var artifact = Artifact.Create(Id, title, type, text);
        _artifacts.Add(artifact);
        return artifact;
    }

    public Transcript AddTranscript(string language)
    {
        var transcript = Transcript.Create(Id, language);
        _transcripts.Add(transcript);
        return transcript;
    }

    #region Private

    private Video()
    { }

    List<VideoTag> _videoTags = new List<VideoTag>();
    List<Playlist> _playLists = new List<Playlist>();
    List<Artifact> _artifacts = new List<Artifact>();
    List<Thumbnail> _thumbnails = new List<Thumbnail>();
    List<Transcript> _transcripts = new List<Transcript>();
    List<PlaylistVideo> _playlistVideos = new List<PlaylistVideo>();

    #endregion
}
