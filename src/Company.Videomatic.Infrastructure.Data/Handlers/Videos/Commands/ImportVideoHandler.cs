namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public sealed class ImportVideoHandler : BaseRequestHandler<ImportVideoCommand, ImportVideoResponse>
{
    public ImportVideoHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<ImportVideoResponse> Handle(ImportVideoCommand request, CancellationToken cancellationToken = default)
    {
        Video dbVideo = Mapper.Map<ImportVideoCommand, Video>(request);

        var entry = DbContext.Add(dbVideo);
        var res = await DbContext.SaveChangesAsync(cancellationToken);

        //_dbContext.ChangeTracker.Clear();

        return new ImportVideoResponse(true, VideoId: entry.Entity.Id);
    }
}