using Company.SharedKernel.CQRS.Commands;

namespace Company.Videomatic.Application.Features.Artifacts.Commands;

public record UpdateArtifactCommand(int Id,
                                    string Name,
                                    string? Text) : UpdateEntityCommand<Artifact>(Id);

public class UpdateArtifactCommandValidator : AbstractValidator<UpdateArtifactCommand>
{
    public UpdateArtifactCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
    }
}
