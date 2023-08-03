namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public sealed class UpdateVideoHandler : UpdateAggregateRootHandler<UpdateVideoCommand, Video, VideoId>
{
    public UpdateVideoHandler(IRepository<Video> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    protected override VideoId ConvertIdOfRequest(UpdateVideoCommand request) => request.Id;
}
