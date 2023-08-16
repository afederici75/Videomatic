using SharedKernel.CQRS.Commands;

namespace Application.Features.Artifacts.Commands;

public readonly record struct UpdateArtifactCommand(ArtifactId Id,
                                    string Name,
                                    string? Text) : IRequest<Result<Artifact>>;

public class UpdateArtifactCommandValidator : AbstractValidator<UpdateArtifactCommand>
{
    public UpdateArtifactCommandValidator()
    {
        RuleFor(x => (int)x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
    }
}
