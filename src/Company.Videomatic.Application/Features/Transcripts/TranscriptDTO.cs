namespace Company.Videomatic.Application.Features.Transcripts;

public record TranscriptDTO(
    long Id = 0,
    long VideoId = 0,
    string Language = "",
    TranscriptLineDTO[]? Lines = null,
    int? LineCount = 0);
