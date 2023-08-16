namespace Application.Features.Transcripts.Commands;

public class DeleteTranscriptCommand(TranscriptId Id) : IRequest<Result>
{ 
    public TranscriptId Id { get; } = Id;
}

internal class DeleteTranscriptCommandValidator : AbstractValidator<DeleteTranscriptCommand>
{
    public DeleteTranscriptCommandValidator()
    {
        RuleFor(x => (int)x.Id).GreaterThan(0);
    }
}