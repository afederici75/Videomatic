namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public sealed class CreateVideoHandler : CreateAggregateRootHandler<CreateVideoCommand, Video>
{
    public CreateVideoHandler(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
    {
    }
}
