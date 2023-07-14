namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public sealed class CreateVideoHandler : CreateEntityHandlerBase<CreateVideoCommand, CreateVideoResponse, Video>
{
    public CreateVideoHandler(IRepository<Video> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    protected override CreateVideoResponse CreateResponseFor(Video entity)
        => new CreateVideoResponse(Id: entity.Id);
}
