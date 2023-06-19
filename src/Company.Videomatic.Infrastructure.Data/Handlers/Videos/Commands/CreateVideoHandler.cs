namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public sealed class CreateVideoHandler : BaseRequestHandler<CreateVideoCommand, CreateVideoResponse>
{
    public CreateVideoHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<CreateVideoResponse> Handle(CreateVideoCommand request, CancellationToken cancellationToken = default)
    {
        Video dbVideo = Mapper.Map<CreateVideoCommand, Video>(request);

        var entry = DbContext.Add(dbVideo);
        var res = await DbContext.SaveChangesAsync(cancellationToken);

        //_dbContext.ChangeTracker.Clear();

        return new CreateVideoResponse(Id: entry.Entity.Id);
    }
}
