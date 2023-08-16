namespace Application.Features.Transcripts;

public class TranscriptDTO(
    int id = 0,
    int videoId = 0,
    string language = "",
    TranscriptLineDTO[]? lines = null,
    int? lineCount = 0)
{ 
    public int Id { get; } = id;
    public int VideoId { get; } = videoId;
    public string Language { get; } = language;
    public TranscriptLineDTO[]? Lines { get; } = lines;
    public int? LineCount { get; } = lineCount;
}
