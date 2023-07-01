namespace Company.Videomatic.Domain.Videos;

public record VideoId(long Value = 0)
{
    public static implicit operator long(VideoId x) => x.Value;
    public static implicit operator VideoId(long x) => new VideoId(x);
}

public class Video : IAggregateRoot
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
    public IReadOnlyCollection<Thumbnail> Thumbnails => _thumbnails.ToList();    
    public IReadOnlyCollection<PlaylistVideo> PlaylistVideos => _playlistVideos.ToList();
    public IReadOnlyCollection<Artifact> Artifacts => _artifacts.ToList();    
    public IReadOnlyCollection<Transcript> Transcripts => _transcripts.ToList();

    public bool AddTag(string name)
    {
        return _videoTags.Add(name);        
    }
    
    public void AddThumbnail(string location, ThumbnailResolution resolution, int height, int width)
    {        
        var res = _thumbnails.FirstOrDefault(t => t.Resolution==resolution);
        if (res is not null)
        { 
            _thumbnails.Remove(res);
        }

        _thumbnails.Add(new Thumbnail(location, resolution, height, width));
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

    HashSet<VideoTag> _videoTags = new ();
    HashSet<Thumbnail> _thumbnails = new ();

    List<Artifact> _artifacts = new ();    
    List<Transcript> _transcripts = new ();
    List<PlaylistVideo> _playlistVideos = new ();

    #endregion
}
