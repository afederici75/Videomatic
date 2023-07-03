namespace Company.Videomatic.Application.Features.Transcript.Commands;

public record UpdateTranscriptCommand(
    long VideoId,
    string Language,
    TranscriptLinePayload[] Lines) : IRequest<UpdateTranscriptResponse>;


public record class TranscriptLinePayload(
   string Text,
   TimeSpan? StartsAt = default,
   TimeSpan? Duration = default)
{
    public static implicit operator string(TranscriptLinePayload x) => x.Text;
    public static implicit operator TranscriptLinePayload(string x) => new TranscriptLinePayload(x, null, null);
}

public record UpdateTranscriptResponse(
    long TranscriptId);