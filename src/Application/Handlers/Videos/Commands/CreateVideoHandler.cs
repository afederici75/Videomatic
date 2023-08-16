using SharedKernel.CQRS.Commands;
using Domain.Videos;

namespace Application.Handlers.Videos.Commands;

public sealed class CreateVideoHandler : CreateEntityHandler<CreateVideoCommand, Video>
{
    public CreateVideoHandler(IRepository<Video> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
