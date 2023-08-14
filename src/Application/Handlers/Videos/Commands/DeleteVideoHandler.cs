using SharedKernel.CQRS.Commands;
using Domain.Videos;

namespace Application.Handlers.Videos.Commands;

public sealed class DeleteVideoHandler : DeleteEntityHandler<DeleteVideoCommand, Video, VideoId>
{
    public DeleteVideoHandler(IRepository<Video> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
