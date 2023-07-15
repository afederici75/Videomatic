namespace Company.Videomatic.Application.Features.Transcripts.Commands;

public record DeleteTranscriptCommand(long Id) : DeleteAggregateRootCommand<Transcript>(Id);

public class DeleteTranscriptCommandValidator : AbstractValidator<DeleteTranscriptCommand>
{
    public DeleteTranscriptCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}