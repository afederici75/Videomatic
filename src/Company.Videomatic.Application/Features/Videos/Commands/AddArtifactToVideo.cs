namespace Company.Videomatic.Application.Features.Videos.Commands;

public record AddArtifactToVideoCommand(long VideoId, string Title, string Type, string? Text = null) : IRequest<AddArtifactToVideoResponse>;

public record AddArtifactToVideoResponse(long VideoId, long Id);
