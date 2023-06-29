namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public sealed class CreateVideoHandler : BaseRequestHandler<CreateVideoCommand, CreatedResponse>
{
    public CreateVideoHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<CreatedResponse> Handle(CreateVideoCommand request, CancellationToken cancellationToken = default)
    {
        Video video = Mapper.Map<CreateVideoCommand, Video>(request);

        //var details = new VideoDetails(
        //    Provider: "YOUTUBE",            
        //    ); 

        var entry = DbContext.Add(video);
        var res = await DbContext.CommitChangesAsync(cancellationToken);

        return new CreatedResponse(Id: entry.Entity.Id);
    }
}