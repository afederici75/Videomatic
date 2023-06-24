namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public sealed class UpdateVideoHandler : BaseRequestHandler<UpdateVideoCommand, UpdatedResponse>
{
    public UpdateVideoHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<UpdatedResponse> Handle(UpdateVideoCommand request, CancellationToken cancellationToken = default)
    {
        var newValue = Mapper.Map<UpdateVideoCommand, Video>(request);

        var playlistDb = await DbContext.Videos
            .AsTracking()
            .SingleAsync(x => x.Id == request.Id, cancellationToken);

        Mapper.Map(request, playlistDb);

        var cnt = await DbContext.CommitChangesAsync(cancellationToken);

        return new UpdatedResponse(request.Id, cnt > 0);
    }
}
