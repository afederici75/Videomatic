namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public sealed class DeleteVideoHandler : DeleteAggregateRootHandler<DeleteVideoCommand, Video, VideoId>
{
    public DeleteVideoHandler(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
    {
    }

    protected override VideoId ConvertIdOfRequest(DeleteVideoCommand request) => request.Id;
}
