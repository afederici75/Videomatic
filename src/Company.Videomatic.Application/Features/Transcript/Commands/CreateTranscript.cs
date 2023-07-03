using Company.Videomatic.Application.Features.Artifact.Commands;

namespace Company.Videomatic.Application.Features.Transcript.Commands;

public record CreateTranscriptCommand(long VideoId, string Language) : IRequest<CreateTranscriptResponse>;

public record CreateTranscriptResponse(long Id);

public class CreateTranscriptCommandValidatorx : AbstractValidator<CreateTranscriptCommand>
{
    public CreateTranscriptCommandValidatorx()
    {
        RuleFor(x => x.VideoId).GreaterThan(0);
        RuleFor(x => x.Language).NotEmpty();        
    }
}

