namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public sealed class UpdateVideoHandler : UpdateAggregateRootHandler<UpdateVideoCommand, Video, VideoId>
{
    public UpdateVideoHandler(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
    {
    }

    protected override VideoId ConvertIdOfRequest(UpdateVideoCommand request) => request.Id;
}
