using System.Collections.Immutable;

namespace Company.Videomatic.Infrastructure.Data.Model;

public class Video : EntityBase
{
    public Video(string location, string title, string? description)
    {
        Location = location;
        Title = title;
        Description = description;
    }

    public string Location { get; }
    public string Title { get; }
    public string? Description { get; }

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

    public Video AddTag(string name)
    {
        var newTag = VideoTag.Create(Id, name);
        _videoTags.Add(newTag);
        return this;
    }

    public Video AddThumbnail(Thumbnail thumbnail)
    {
        _thumbnails.Add(thumbnail);
        return this;
    }   

    public Video AddArtifact(Artifact artifact)
    {
        _artifacts.Add(artifact);
        return this;
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

    public Video AddTranscript(Transcript transcript)
    {
        _transcripts.Add(transcript);
        return this;
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
