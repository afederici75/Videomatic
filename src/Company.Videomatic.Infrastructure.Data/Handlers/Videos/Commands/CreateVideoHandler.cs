namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public sealed class CreateVideoHandler : BaseRequestHandler<CreateVideoCommand, CreatedResponse>
{
    public CreateVideoHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<CreatedResponse> Handle(CreateVideoCommand request, CancellationToken cancellationToken = default)
    {
        Video dbVideo = Mapper.Map<CreateVideoCommand, Video>(request);

        var entry = DbContext.Add(dbVideo);
        var res = await DbContext.SaveChangesAsync(cancellationToken);

        return new CreatedResponse(Id: entry.Entity.Id);
    }
}
