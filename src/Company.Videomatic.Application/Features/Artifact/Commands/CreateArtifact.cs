namespace Company.Videomatic.Application.Features.Artifact.Commands;

public record CreateArtifactCommand(long VideoId,
                                    string Title,
                                    string Type,
                                    string? Text) : IRequest<CreateArtifactResponse>;

public record CreateArtifactResponse(long Id);

public class CreateArtifactCommandValidatorx : AbstractValidator<CreateArtifactCommand>
{
    public CreateArtifactCommandValidatorx()
    {
        RuleFor(x => x.VideoId).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Type).NotEmpty();
        RuleFor(x => x.Text).NotEmpty();
    }
}