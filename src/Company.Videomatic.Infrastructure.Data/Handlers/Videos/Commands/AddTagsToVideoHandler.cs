namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public class AddTagsToVideoHandler : BaseRequestHandler<AddTagsToVideoCommand, AddTagsToVideoResponse>
{
    public AddTagsToVideoHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<AddTagsToVideoResponse> Handle(AddTagsToVideoCommand request, CancellationToken cancellationToken = default)
    {
        var currentUserVideoTags = await DbContext.VideoTags
                .Where(x => x.VideoId == request.VideoId)
                .ToListAsync(cancellationToken);

        throw new NotImplementedException();
    }
}
