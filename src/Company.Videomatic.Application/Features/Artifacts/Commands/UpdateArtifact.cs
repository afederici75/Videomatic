namespace Company.Videomatic.Application.Features.Artifacts.Commands;

public record UpdateArtifactCommand(long Id,
                                    string Name,
                                    string? Text) : IRequest<UpdateArtifactResponse>, ICommandWithEntityId;

public record UpdateArtifactResponse(long ArtifactId, bool wasUpdated);

public class UpdateArtifactCommandValidator : AbstractValidator<UpdateArtifactCommand>
{
    public UpdateArtifactCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
    }
}
