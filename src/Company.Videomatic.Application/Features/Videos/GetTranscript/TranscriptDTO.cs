namespace Company.Videomatic.Application.Features.Videos.GetTranscript;

public record TranscriptDTO(int VideoId, int TranscriptId, string Language, string[] Lines, int LineCount);

