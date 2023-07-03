using Company.Videomatic.Application.Features.Artifacts.Commands;

namespace Company.Videomatic.Application.Features.Transcripts.Commands;

public record CreateTranscriptCommand(long VideoId, string Language, IEnumerable<string> Lines) : IRequest<CreateTranscriptResponse>;

public record CreateTranscriptResponse(long Id);

public class CreateTranscriptCommandValidatorx : AbstractValidator<CreateTranscriptCommand>
{
    public CreateTranscriptCommandValidatorx()
    {
        RuleFor(x => x.VideoId).GreaterThan(0);
        RuleFor(x => x.Language).NotEmpty();
        RuleFor(x => x.Lines).NotEmpty();
    }
}

