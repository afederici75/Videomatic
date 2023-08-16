namespace Application.Features.Artifacts.Commands;

public class CreateArtifactCommand(VideoId videoId, string name, string type, string? text) : IRequest<Result<Artifact>>
{
    public readonly VideoId VideoId = videoId;
    public readonly string Name = name;
    public readonly string Type = type;
    public readonly string? Text  = text;

    internal class CreateArtifactCommandValidator : AbstractValidator<CreateArtifactCommand>
    {
        public CreateArtifactCommandValidator()
        {
            RuleFor(x => (int)x.VideoId).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Text).NotEmpty();
        }
    }
}

