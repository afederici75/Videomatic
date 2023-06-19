using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public class AddTagsToVideoHandler : BaseRequestHandler<AddTagsToVideoCommand, AddTagsToVideoResponse>
{
    public AddTagsToVideoHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<AddTagsToVideoResponse> Handle(AddTagsToVideoCommand request, CancellationToken cancellationToken = default)
    {
        var currentUserVideoTags = DbContext.VideoTags
                .Where(x => x.VideoId == request.VideoId)
                .Select(x => x.Name);

        var validTags = request.Tags.Except(currentUserVideoTags)
            .ToList();

        foreach (var tag in validTags)
        {
            DbContext.Add(new VideoTag(request.VideoId, tag));              
        }

        await DbContext.SaveChangesAsync(cancellationToken);

        return new AddTagsToVideoResponse(request.VideoId, validTags);
    }
}
