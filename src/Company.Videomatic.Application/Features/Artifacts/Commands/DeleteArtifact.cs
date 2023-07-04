namespace Company.Videomatic.Application.Features.Artifacts.Commands;

public record DeleteArtifactCommand(long Id) : IRequest<DeleteArtifactResponse>, ICommandWithEntityId;

public record DeleteArtifactResponse(long Id, bool Deleted);

public class DeleteArtifactCommandValidator : AbstractValidator<DeleteArtifactCommand>
{
    public DeleteArtifactCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}