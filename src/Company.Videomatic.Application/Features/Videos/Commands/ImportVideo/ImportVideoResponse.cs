using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Application.Features.Videos.Commands.ImportVideo;

/// <summary>
/// The response returned by ImportVideoCommand.
/// </summary>
/// <param name="VideoId"></param>
public record ImportVideoResponse(int VideoId, int ThumbNailCount, int TranscriptCount, int ArtifactsCount);