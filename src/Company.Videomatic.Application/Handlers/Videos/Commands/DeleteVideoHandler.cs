using Company.SharedKernel.CQRS.Commands;
using Company.Videomatic.Domain.Video;

namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public sealed class DeleteVideoHandler : DeleteEntityHandler<DeleteVideoCommand, Video, VideoId>
{
    public DeleteVideoHandler(IRepository<Video> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
