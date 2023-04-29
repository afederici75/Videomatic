namespace Company.Videomatic.Application.Features.Videos.Queries;

public record GetTranscriptDTOQuery(int TranscriptId) : IRequest<TranscriptDTO>;
