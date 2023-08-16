using SharedKernel.CQRS.Commands;

namespace Application.Features.Transcripts.Commands;

public readonly record struct CreateTranscriptCommand(int VideoId,
                                      string Language,
                                      IEnumerable<string> Lines) : IRequest<Result<Transcript>>;

public class CreateTranscriptCommandValidator : AbstractValidator<CreateTranscriptCommand>
{
    public CreateTranscriptCommandValidator()
    {
        RuleFor(x => x.VideoId).GreaterThan(0);
        RuleFor(x => x.Language).NotEmpty();
        RuleFor(x => x.Lines).NotEmpty();
    }
}

