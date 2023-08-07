using Company.SharedKernel.Common.CQRS;

namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public sealed class UpdateVideoHandler : UpdateEntityHandler<UpdateVideoCommand, Video, VideoId>
{
    public UpdateVideoHandler(IRepository<Video> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    protected override VideoId ConvertIdOfRequest(UpdateVideoCommand request) => request.Id;
}
