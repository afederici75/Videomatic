namespace Application.Features.Artifacts.Commands;

public class DeleteArtifactCommand(ArtifactId id) : IRequest<Result>
{
    public ArtifactId Id { get; } = id;

    internal class DeleteArtifactCommandValidator : AbstractValidator<DeleteArtifactCommand>
    {
        public DeleteArtifactCommandValidator()
        {
            RuleFor(x => (int)x.Id).GreaterThan(0);
        }
    }
}