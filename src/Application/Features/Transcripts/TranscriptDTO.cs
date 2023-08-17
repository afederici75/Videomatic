namespace Application.Features.Transcripts;

public class TranscriptDTO(
    int id = 0,
    int videoId = 0,
    string language = "",
    IEnumerable<TranscriptLineDTO>? lines = null,
    int? lineCount = 0)
{ 
    public int Id { get; } = id;
    public int VideoId { get; } = videoId;
    public string Language { get; } = language;
    public IEnumerable<TranscriptLineDTO>? Lines { get; } = lines;
    public int? LineCount { get; } = lineCount;

    public TranscriptDTO(Transcript transcript)
        : this(0, 
              transcript.VideoId, 
              transcript.Language,
              transcript.Lines.Select(x => new TranscriptLineDTO(x.Text, x.StartsAt, x.Duration)))    
    { }
}
