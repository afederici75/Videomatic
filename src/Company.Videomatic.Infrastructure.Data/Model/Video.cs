using System.Collections.Immutable;

namespace Company.Videomatic.Infrastructure.Data.Model;

public class Video : EntityBase
{
    public static Video Create(string location, string title, string? description)
    {
        return new Video
        {
            Location = location,
            Title = title,
            Description = description,
        };
    }

    public string Location { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }

    public IReadOnlyCollection<VideoTag> VideoTags 
    { 
        get => _videoTags.ToList();
        //private set => _videoTags = value.ToList();
    }

    public IReadOnlyCollection<Playlist> Playlists 
    { 
        get => _playLists.ToList();
        //private set => _playLists = value.ToList();
    }
    public IReadOnlyCollection<PlaylistVideo> PlaylistVideos 
    { 
        get => _playlistVideos.ToList();
        //private set => _playlistVideos = value.ToList();
    }

    public IReadOnlyCollection<Artifact> Artifacts 
    { 
        get => _artifacts.ToList();
        //private set => _artifacts = value.ToList();
    }

    public IReadOnlyCollection<Thumbnail> Thumbnails 
    { 
        get => _thumbnails.ToList();
        //private set => _thumbnails = value.ToList();
    }

    public IReadOnlyCollection<Transcript> Transcripts 
    { 
        get => _transcripts.ToList();
        //private set => _transcripts = value.ToList();
    }

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

    public Video AddPlaylistVideo(PlaylistVideo playlistVideo)
    {
        _playlistVideos.Add(playlistVideo);
        return this;
    }   

    public Video AddPlaylist(Playlist playlist)
    {
        _playLists.Add(playlist);
        return this;
    }   

    public Transcript AddTranscript(string language)
    {
        var transcript = Transcript.Create(Id, language);
        _transcripts.Add(transcript);
        return transcript;
    }

    #region Private

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Video()
    { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    List<VideoTag> _videoTags = new List<VideoTag>();
    List<Playlist> _playLists = new List<Playlist>();
    List<Artifact> _artifacts = new List<Artifact>();
    List<Thumbnail> _thumbnails = new List<Thumbnail>();
    List<Transcript> _transcripts = new List<Transcript>();
    List<PlaylistVideo> _playlistVideos = new List<PlaylistVideo>();

    #endregion
}
