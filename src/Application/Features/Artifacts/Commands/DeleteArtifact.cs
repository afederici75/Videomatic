namespace Application.Features.Artifacts.Commands;

public readonly record struct DeleteArtifactCommand(ArtifactId Id) : IRequest<Result>;

internal class DeleteArtifactCommandValidator : AbstractValidator<DeleteArtifactCommand>
{
    public DeleteArtifactCommandValidator()
    {
        RuleFor(x => (int)x.Id).GreaterThan(0);
    }
}