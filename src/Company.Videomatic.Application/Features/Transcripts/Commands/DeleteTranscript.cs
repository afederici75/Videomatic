using Company.SharedKernel.Common.CQRS;

namespace Company.Videomatic.Application.Features.Transcripts.Commands;

public record DeleteTranscriptCommand(int Id) : DeleteAggregateRootCommand<Transcript>(Id);

public class DeleteTranscriptCommandValidator : AbstractValidator<DeleteTranscriptCommand>
{
    public DeleteTranscriptCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}