using Company.SharedKernel.CQRS.Commands;

namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public sealed class CreateVideoHandler : CreateEntitytHandler<CreateVideoCommand, Video>
{
    public CreateVideoHandler(IRepository<Video> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
