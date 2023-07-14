namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public sealed class UpdateVideoHandler : UpdateEntityHandlerBase<UpdateVideoCommand, UpdateVideoResponse, Video, VideoId>
{
    public UpdateVideoHandler(IRepository<Video> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    protected override UpdateVideoResponse CreateResponseFor(VideoId updatedEntityId, bool wasUpdated)
    => new(updatedEntityId, wasUpdated);

    protected override VideoId GetIdOfRequest(UpdateVideoCommand request)
    => request.Id;
}
