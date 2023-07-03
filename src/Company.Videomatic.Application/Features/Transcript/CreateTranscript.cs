namespace Company.Videomatic.Application.Features.Transcript;

public record CreateTranscript(long VideoId, string Language);

public record UpdateTranscript(long transcriptId, string language);