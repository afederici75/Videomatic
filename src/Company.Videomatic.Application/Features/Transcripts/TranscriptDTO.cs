namespace Company.Videomatic.Application.Features.Transcripts;

public record TranscriptDTO(
    long Id = 0,
    long VideoId = 0,
    string Language = "",

    int? LineCount = 0);
