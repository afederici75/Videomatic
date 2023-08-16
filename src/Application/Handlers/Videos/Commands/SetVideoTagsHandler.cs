using Domain.Videos;

namespace Application.Handlers.Videos.Commands;

public class SetVideoTagsHandler : IRequestHandler<SetVideoTagsCommand, Result>
{
    private readonly IRepository<Video> _repository;    

    public SetVideoTagsHandler(IRepository<Video> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));        
    }

    public async Task<Result> Handle(SetVideoTagsCommand request, CancellationToken cancellationToken = default)
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
