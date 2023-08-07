using Company.SharedKernel.CQRS.Commands;

namespace Company.Videomatic.Application.Features.Transcripts.Commands;

public record DeleteTranscriptCommand(int Id) : DeleteEntityCommand<Transcript>(Id);

public class DeleteTranscriptCommandValidator : AbstractValidator<DeleteTranscriptCommand>
{
    public DeleteTranscriptCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}