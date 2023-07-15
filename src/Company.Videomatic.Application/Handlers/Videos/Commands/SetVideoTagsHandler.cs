namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public class SetVideoTagsHandler : IRequestHandler<SetVideoTags, Result<int>>
{
    private readonly IRepository<Video> _repository;
    private readonly IMapper _mapper;

    public SetVideoTagsHandler(IRepository<Video> repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<int>> Handle(SetVideoTags request, CancellationToken cancellationToken = default)
    {        
        var video =await _repository.GetByIdAsync(new VideoId(request.Id), cancellationToken);

        if (video is null)
        {
            return Result<int>.NotFound();
        }

        var validTags = request.Tags.Except(video.Tags.Select(t => t.Name))
            .ToArray();

        video.AddTags(validTags);                        
        
        await _repository.SaveChangesAsync(cancellationToken);

        return validTags.Length;
    }
}
