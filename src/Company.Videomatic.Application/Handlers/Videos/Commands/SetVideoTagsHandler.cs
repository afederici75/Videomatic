namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public class SetVideoTagsHandler : IRequestHandler<SetVideoTags, Result<int>>
{
    private readonly IRepository<Video> _repository;    

    public SetVideoTagsHandler(IRepository<Video> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));        
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
