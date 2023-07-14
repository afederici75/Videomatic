namespace Company.Videomatic.Application.Features.Transcripts.Commands;

public record DeleteTranscriptCommand(long Id) : IDeleteCommand<Transcript>;

public class DeleteTranscriptCommandValidator : AbstractValidator<DeleteTranscriptCommand>
{
    public DeleteTranscriptCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}