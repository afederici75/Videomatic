namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public sealed class ImportVideoHandler : IRequestHandler<ImportVideoCommand, ImportVideoResponse>
{
    private readonly IRepository<Video> _repository;
    private readonly IMapper _mapper;

    public ImportVideoHandler(IRepository<Video> repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));    
    }

    public async Task<ImportVideoResponse> Handle(ImportVideoCommand request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
        //Video dbVideo = Mapper.Map<ImportVideoCommand, Video>(request);
        //
        //var entry = DbContext.Add(dbVideo);
        //var res = await DbContext.CommitChangesAsync(cancellationToken);
        //
        ////_dbContext.ChangeTracker.Clear();
        //
        //return new ImportVideoResponse(true, VideoId: entry.Entity.Id);
    }
}