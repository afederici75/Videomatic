using Newtonsoft.Json;

namespace Company.Videomatic.Domain;

public class Video 
{
    public int Id { get; init; }
    public string ProviderId { get; init; }
    public string VideoUrl { get; init; }

    public string? Title { get; set; }
    public string? Description { get; set; }

    public IEnumerable<Thumbnail> Thumbnails
    {
        get { return _thumbnails.AsReadOnly(); }
        set { _thumbnails = value.ToList(); }
    }
                
    public IEnumerable<Transcript> Transcripts
    {
        get { return _transcripts.AsReadOnly(); }
        set { _transcripts = value?.ToList() ?? new List<Transcript>(); }
    }
    
    private List<Transcript> _transcripts = new List<Transcript>();
    private List<Thumbnail> _thumbnails = new List<Thumbnail>();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonConstructor]
    public Video()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    { 
        // For entity framework
    }

    public Video(string providerId, string videoUrl, string? title = null, string? description = null,
        IEnumerable<Thumbnail>? thumbnails = null, IEnumerable<Transcript>? transcripts = null)
    {
        if (string.IsNullOrWhiteSpace(providerId))
        {
            throw new ArgumentException($"'{nameof(providerId)}' cannot be null or whitespace.", nameof(providerId));
        }

        if (string.IsNullOrWhiteSpace(videoUrl))
        {
            throw new ArgumentException($"'{nameof(videoUrl)}' cannot be null or whitespace.", nameof(videoUrl));
        }

        ProviderId = providerId;
        VideoUrl = videoUrl;
        Title = title;
        Description = description;

        //_thumbnails = thumbnails?.ToList() ?? new List<Thumbnail?> ();
        //_transcripts = transcripts?.ToList() ?? new List<Transcript?>();
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

    public Video ClearTranscripts()
    { 
        _transcripts.Clear();
        return this;
    }

    public Video ClearThumbnails()
    {
        _thumbnails.Clear();
        return this;
    }
}