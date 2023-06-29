using JetBrains.Annotations;

namespace Company.Videomatic.Domain.Videos;

public record VideoDetails(
    string Provider,
    DateTime VideoPublishedAt,
    string ChannelId,
    string PlaylistId,
    int Position,
    string VideoOwnerChannelTitle,
    string VideoOwnerChannelId
    )
{
    private VideoDetails() :
        this(Provider: "NONE",
             VideoPublishedAt: DateTime.UtcNow,
             ChannelId: "",
             PlaylistId: "",
             Position: 0,
             VideoOwnerChannelTitle: "",
             VideoOwnerChannelId: "") { }

    public static VideoDetails CreateEmpty()
    {
        return new VideoDetails();
    }
}

public class Video : EntityBase
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

    //public void SetDetails(VideoDetails details)
    //{ 
    //    this.Details = details;
    //}

    public VideoTag AddTag(string name)
    {
        var newTag = VideoTag.Create(Id, name);
        _videoTags.Add(newTag);
        return newTag;
    }

    public Thumbnail AddThumbnail(string location, ThumbnailResolution resolution, int height, int width)
    {
        var thumbnail = Thumbnail.Create(Id, location, resolution, height, width);
        _thumbnails.Add(thumbnail);
        return thumbnail;
    }

    public Artifact AddArtifact(string title, string type, string? text = null)
    {
        var artifact = Artifact.Create(Id, title, type, text);
        _artifacts.Add(artifact);
        return artifact;
    }

    //public Video AddPlaylistVideo(PlaylistVideo playlistVideo)
    //{
    //    _playlistVideos.Add(playlistVideo);
    //    return this;
    //}

    //public Video AddPlaylist(Playlist playlist)
    //{
    //    _playLists.Add(playlist);
    //    return this;
    //}

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
