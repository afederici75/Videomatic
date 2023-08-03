namespace Company.Videomatic.Application.Features.Transcripts;

public record TranscriptDTO(
    int Id = 0,
    int VideoId = 0,
    string Language = "",
    TranscriptLineDTO[]? Lines = null,
    int? LineCount = 0);
