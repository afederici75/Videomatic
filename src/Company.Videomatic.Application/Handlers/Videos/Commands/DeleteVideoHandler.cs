using Company.Videomatic.Domain.Specifications.Videos;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public sealed class DeleteVideoHandler : DeleteEntityHandlerBase<DeleteVideoCommand, DeleteVideoResponse, Video, VideoId>
{
    public DeleteVideoHandler(IRepository<Video> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    protected override DeleteVideoResponse CreateResponseFor(VideoId entityId, bool wasDeleted)
        => new(entityId, wasDeleted);

    protected override VideoId GetIdOfRequest(DeleteVideoCommand request)
        => new(request.Id);
}
