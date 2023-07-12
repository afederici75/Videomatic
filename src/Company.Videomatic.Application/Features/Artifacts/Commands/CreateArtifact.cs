using Ardalis.Result;

namespace Company.Videomatic.Application.Features.Artifacts.Commands;

public record CreateArtifactCommand(long Id,
                                    string Name,
                                    string Type,
                                    string? Text) : IRequest<Result<CreateArtifactResponse>>, ICommandWithEntityId;

public record CreateArtifactResponse(long Id);

public class CreateArtifactCommandValidator : AbstractValidator<CreateArtifactCommand>
{
    public CreateArtifactCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Type).NotEmpty();
        RuleFor(x => x.Text).NotEmpty();
    }
}