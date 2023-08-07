using Company.SharedKernel.CQRS.Commands;

namespace Company.Videomatic.Application.Features.Artifacts.Commands;

public record DeleteArtifactCommand(int Id) : DeleteEntityCommand<Artifact>(Id);

public class DeleteArtifactCommandValidator : AbstractValidator<DeleteArtifactCommand>
{
    public DeleteArtifactCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}