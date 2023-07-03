namespace Company.Videomatic.Application.Features.Artifact.Commands;

public record DeleteArtifactCommand(long Id) : IRequest<DeleteArtifactResponse>;

public record DeleteArtifactResponse(long Id, bool Deleted);

public class DeleteArtifactCommandValidator : AbstractValidator<DeleteArtifactCommand>
{
    public DeleteArtifactCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}