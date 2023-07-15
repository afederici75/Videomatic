namespace Company.Videomatic.Application.Features.Artifacts.Commands;

public record UpdateArtifactCommand(long Id,
                                    string Name,
                                    string? Text) : UpdateAggregateRootCommand<Artifact>(Id);

public class UpdateArtifactCommandValidator : AbstractValidator<UpdateArtifactCommand>
{
    public UpdateArtifactCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
    }
}
