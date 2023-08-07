using Company.SharedKernel.CQRS.Commands;

namespace Company.Videomatic.Application.Features.Artifacts.Commands;

public record CreateArtifactCommand(int VideoId,
                                    string Name,
                                    string Type,
                                    string? Text) : CreateEntityCommand<Artifact>();

public class CreateArtifactCommandValidator : AbstractValidator<CreateArtifactCommand>
{
    public CreateArtifactCommandValidator()
    {
        RuleFor(x => x.VideoId).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Type).NotEmpty();
        RuleFor(x => x.Text).NotEmpty();
    }
}