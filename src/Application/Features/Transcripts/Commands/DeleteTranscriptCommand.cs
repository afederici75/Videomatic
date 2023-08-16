namespace Application.Features.Transcripts.Commands;

public class DeleteTranscriptCommand(TranscriptId id) : IRequest<Result>
{ 
    public TranscriptId Id { get; } = id;

    internal class DeleteTranscriptCommandValidator : AbstractValidator<DeleteTranscriptCommand>
    {
        public DeleteTranscriptCommandValidator()
        {
            RuleFor(x => (int)x.Id).GreaterThan(0);
        }
    }
}