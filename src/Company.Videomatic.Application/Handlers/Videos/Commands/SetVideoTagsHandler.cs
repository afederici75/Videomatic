﻿namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public class SetVideoTagsHandler : IRequestHandler<SetVideoTags, SetVideoTagsResponse>
{
    private readonly IRepository<Video> _repository;
    private readonly IMapper _mapper;

    public SetVideoTagsHandler(IRepository<Video> repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<SetVideoTagsResponse> Handle(SetVideoTags request, CancellationToken cancellationToken = default)
    {
        var spec = new VideosByIdSpecification(request.Id); 

        var video = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (video is null)
        {
            return new SetVideoTagsResponse(request.Id, 0);
        }

        var validTags = request.Tags.Except(video.Tags.Select(t => t.Name))
            .ToArray();

        video.AddTags(validTags);                        
        
        await _repository.SaveChangesAsync(cancellationToken);

        return new SetVideoTagsResponse(request.Id, validTags.Length);
    }
}
