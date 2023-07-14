namespace Company.Videomatic.Application.Features.Artifacts.Commands;

public record DeleteArtifactCommand(long Id) : IDeleteCommand<Artifact>;

public class DeleteArtifactCommandValidator : AbstractValidator<DeleteArtifactCommand>
{
    public DeleteArtifactCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}