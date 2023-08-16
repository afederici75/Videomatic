namespace Application.Features.Artifacts.Commands;

public record DeleteArtifactCommand(ArtifactId Id) : IRequest<Result>;

public class DeleteArtifactCommandValidator : AbstractValidator<DeleteArtifactCommand>
{
    public DeleteArtifactCommandValidator()
    {
        RuleFor(x => (int)x.Id).GreaterThan(0);
    }
}