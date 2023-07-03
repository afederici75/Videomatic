namespace Company.Videomatic.Application.Features.Transcripts.Commands;

public record DeleteTranscriptCommand(long Id) : IRequest<DeleteTranscriptResponse>;

public record DeleteTranscriptResponse(long Id, bool Deleted);

public class DeleteTranscriptCommandValidator : AbstractValidator<DeleteTranscriptCommand>
{
    public DeleteTranscriptCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}