using MediatR;

namespace Company.Videomatic.Infrastructure.Data.Handlers;

public abstract class BaseRequestHandler<TREQUEST, TRESPONSE> : IRequestHandler<TREQUEST, TRESPONSE>
    where TREQUEST : IRequest<TRESPONSE>
{
    protected readonly VideomaticDbContext DbContext;
    protected readonly IMapper Mapper;

    public BaseRequestHandler(VideomaticDbContext dbContext, IMapper mapper)
    {
        DbContext = dbContext;
        Mapper = mapper;
    }

    public abstract Task<TRESPONSE> Handle(TREQUEST request, CancellationToken cancellationToken);
}
