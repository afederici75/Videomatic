namespace Company.Videomatic.Application.Features.Videos.Queries;

public record TranscriptDTO(int VideoId, int TranscriptId, string Language, string[] Lines, int LineCount);

