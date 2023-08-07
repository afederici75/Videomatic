using Company.SharedKernel.CQRS.Commands;

namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public sealed class UpdateVideoHandler : UpdateEntityHandler<UpdateVideoCommand, Video, VideoId>
{
    public UpdateVideoHandler(IRepository<Video> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
