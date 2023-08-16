namespace Application.Features.Artifacts.Commands;

public readonly record struct CreateArtifactCommand(VideoId VideoId,
                                    string Name,
                                    string Type,
                                    string? Text) : IRequest<Result<Artifact>>;

public class CreateArtifactCommandValidator : AbstractValidator<CreateArtifactCommand>
{
    public CreateArtifactCommandValidator()
    {
        RuleFor(x => (int)x.VideoId).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Type).NotEmpty();
        RuleFor(x => x.Text).NotEmpty();
    }
}