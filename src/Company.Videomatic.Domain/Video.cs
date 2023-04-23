namespace Company.Videomatic.Domain;

public class Video 
{
    public int Id { get; private set; }
    public string ProviderId { get; private set; }
    public string VideoUrl { get; private set; }

    public string? Title { get; set; }
    public string? Description { get; set; }

    public IReadOnlyList<Thumbnail> Thumbnails => _thumbnails.AsReadOnly();
    public IReadOnlyList<Transcript?> Transcripts => _transcripts.AsReadOnly();

    private readonly List<Transcript?> _transcripts = new List<Transcript?>();
    private readonly List<Thumbnail> _thumbnails = new List<Thumbnail>();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Video()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    { 
        // For entity framework
    }

    public Video(string providerId, string videoUrl, string? title = null, string? description = null)
    {
        ProviderId = providerId;
        VideoUrl = videoUrl;
        Title = title;
        Description = description;
    }

    public Video AddThumbnails(params Thumbnail[] thumbnails)
    {
        foreach (var thumbnail in thumbnails)
        {
            thumbnail.SetVideo(this);

        }
        _thumbnails.AddRange(thumbnails);
        
        return this;
    }

    public Video AddTranscripts(params Transcript[] transcripts)
    {
        foreach (var transcript in transcripts)
        { 
            transcript.SetVideo(this);  
        }
        _transcripts.AddRange(transcripts);

        return this;
    }
}