using Company.Videomatic.Domain.Entities.TranscriptAggregate;

namespace Company.Videomatic.Application.Features.Videos.Commands;

public record AddTranscriptsToVideoCommand(
    long VideoId,
    TranscriptPayload[] Transcripts) : IRequest<AddTranscriptsToVideoResponse>;

public record class TranscriptPayload(
   string Language,
   IEnumerable<TranscriptLinePayload> Lines);

public record class TranscriptLinePayload(
   string Text, 
   TimeSpan StartsAt = default, 
   TimeSpan Duration = default);

public record AddTranscriptsToVideoResponse(
    long VideoId,
    IReadOnlyDictionary<string, TranscriptId> Ids);