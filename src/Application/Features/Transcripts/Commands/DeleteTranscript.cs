namespace Application.Features.Transcripts.Commands;

public readonly record struct DeleteTranscriptCommand(int Id) : IRequest<Result>;

internal class DeleteTranscriptCommandValidator : AbstractValidator<DeleteTranscriptCommand>
{
    public DeleteTranscriptCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}