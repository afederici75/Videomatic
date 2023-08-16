namespace Application.Features.Transcripts.Commands;

public record DeleteTranscriptCommand(int Id) : IRequest<Result>;

public class DeleteTranscriptCommandValidator : AbstractValidator<DeleteTranscriptCommand>
{
    public DeleteTranscriptCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}