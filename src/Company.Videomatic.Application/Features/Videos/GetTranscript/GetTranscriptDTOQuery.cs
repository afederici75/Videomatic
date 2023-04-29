namespace Company.Videomatic.Application.Features.Videos.GetTranscript;

public record GetTranscriptDTOQuery(int TranscriptId) : IRequest<TranscriptDTO>;
