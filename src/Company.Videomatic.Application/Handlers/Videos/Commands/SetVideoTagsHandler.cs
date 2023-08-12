namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public class SetVideoTagsHandler : IRequestHandler<SetVideoTags, Result>
{
    private readonly IRepository<Video> _repository;    

    public SetVideoTagsHandler(IRepository<Video> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));        
    }

    public async Task<Result> Handle(SetVideoTags request, CancellationToken cancellationToken = default)
    {        
        var video =await _repository.GetByIdAsync(new VideoId(request.Id), cancellationToken);

        if (video is null)
        {
            return Result.NotFound();
        }

        video.SetTags(request.Tags);
        
        await _repository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
