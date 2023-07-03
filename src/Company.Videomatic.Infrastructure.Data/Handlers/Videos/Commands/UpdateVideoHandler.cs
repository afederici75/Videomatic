using Company.Videomatic.Domain.Aggregates.Video;
using Company.Videomatic.Domain.Specifications;
using MediatR;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public sealed class UpdateVideoHandler : IRequestHandler<UpdateVideoCommand, UpdateVideoResponse>
{
    private readonly IRepository<Video> _repository;
    private readonly IMapper _mapper;

    public UpdateVideoHandler(IRepository<Video> repository, IMapper mapper) //: base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UpdateVideoResponse> Handle(UpdateVideoCommand request, CancellationToken cancellationToken = default)
    {        
        Video? video = await _repository.GetByIdAsync<VideoId>(new (request.Id), cancellationToken);
        if (video == null)
        {
            return new UpdateVideoResponse(request.Id);
        }

        _mapper.Map<UpdateVideoCommand, Video>(request, video);
        var cnt = await _repository.SaveChangesAsync();
        
        return new UpdateVideoResponse(request.Id);
    }
}
