namespace Company.Videomatic.Application.Features.Videos.Commands;

public record UpdateArtifactCommand(long VideoId, string Title, string Type, string? Text = null) : IRequest<UpdateArtifactResponse>;

public record UpdateArtifactResponse(long ArtifactId);
