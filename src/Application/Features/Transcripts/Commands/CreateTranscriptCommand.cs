using SharedKernel.CQRS.Commands;

namespace Application.Features.Transcripts.Commands;

public class CreateTranscriptCommand(VideoId videoId,
                                      string language,
                                      IEnumerable<string> lines) : IRequest<Result<Transcript>>
{ 
    public VideoId VideoId { get; } = videoId;
    public string Language { get; } = language;
    public IEnumerable<string> Lines { get; } = lines;

    internal class Validator : AbstractValidator<CreateTranscriptCommand>
    {
        public Validator()
        {
            RuleFor(x => (int)x.VideoId).GreaterThan(0);
            RuleFor(x => x.Language).NotEmpty();
            RuleFor(x => x.Lines).NotEmpty();
        }
    }
}