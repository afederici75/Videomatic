namespace Application.Features.Transcripts;

public readonly record struct TranscriptDTO(
    int Id = 0,
    int VideoId = 0,
    string Language = "",
    TranscriptLineDTO[]? Lines = null,
    int? LineCount = 0);
