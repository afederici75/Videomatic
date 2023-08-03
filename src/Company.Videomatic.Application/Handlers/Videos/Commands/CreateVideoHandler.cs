using AutoMapper;
using Company.SharedKernel.Abstractions;

namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public sealed class CreateVideoHandler : CreateAggregateRootHandler<CreateVideoCommand, Video>
{
    public CreateVideoHandler(IRepository<Video> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
