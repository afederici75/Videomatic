namespace Company.Videomatic.Application.Features.Artifact.Commands;

public record UpdateArtifactCommand(long Id,
                                    string Title,
                                    string? Text) : IRequest<UpdateArtifactResponse>;

public record UpdateArtifactResponse(long ArtifactId);

public class UpdateArtifactCommandValidator : AbstractValidator<UpdateArtifactCommand>
{
    public UpdateArtifactCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty();
    }
}
