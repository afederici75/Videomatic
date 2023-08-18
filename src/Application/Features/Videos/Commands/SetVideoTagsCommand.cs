namespace Application.Features.Videos.Commands;

public class SetVideoTagsCommand(VideoId Id, string[] Tags) : IRequest<Result>
{
    public VideoId Id { get; } = Id;
    public string[] Tags { get; } = Tags;

    internal class Validator : AbstractValidator<SetVideoTagsCommand>
    {
        public Validator()
        {
            RuleFor(x => (int)x.Id).GreaterThan(0);
            RuleFor(x => x.Tags).NotNull();
        }
    }

    internal class Handler(IMyRepository<Video> repository) : IRequestHandler<SetVideoTagsCommand, Result>
    {
        private readonly IMyRepository<Video> _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public async Task<Result> Handle(SetVideoTagsCommand request, CancellationToken cancellationToken = default)
        {
            var video = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (video is null)
            {
                return Result.NotFound();
            }

            video.SetTags(request.Tags);

            await _repository.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}