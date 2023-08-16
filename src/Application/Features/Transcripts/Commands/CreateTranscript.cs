using SharedKernel.CQRS.Commands;

namespace Application.Features.Transcripts.Commands;

public class CreateTranscriptCommand(VideoId VideoId,
                                      string Language,
                                      IEnumerable<string> Lines) : IRequest<Result<Transcript>>
{ 
    public VideoId VideoId { get; } = VideoId;
    public string Language { get; } = Language;
    public IEnumerable<string> Lines { get; } = Lines;

    internal class CreateTranscriptCommandValidator : AbstractValidator<CreateTranscriptCommand>
    {
        public CreateTranscriptCommandValidator()
        {
            RuleFor(x => (int)x.VideoId).GreaterThan(0);
            RuleFor(x => x.Language).NotEmpty();
            RuleFor(x => x.Lines).NotEmpty();
        }
    }
}