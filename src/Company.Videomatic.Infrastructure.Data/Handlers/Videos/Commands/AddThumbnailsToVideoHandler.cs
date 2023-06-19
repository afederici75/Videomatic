using Company.Videomatic.Application.Features.Model;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public class AddThumbnailsToVideoHandler : BaseRequestHandler<AddThumnbailsToVideoCommand, AddThumbnailsToVideoResponse>
{
    public AddThumbnailsToVideoHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<AddThumbnailsToVideoResponse> Handle(AddThumnbailsToVideoCommand request, CancellationToken cancellationToken = default)
    {
        var currentUserVideoThumbnail = await DbContext.PlaylistVideos
                .Where(x => x.VideoId == request.VideoId)
                .ToListAsync(cancellationToken);

        return new AddThumbnailsToVideoResponse(-1, new Dictionary<ThumbnailResolution, long>());
    }
}
