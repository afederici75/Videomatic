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
        var video = await DbContext.Videos
                .Where(x => x.Id == request.VideoId)                
                .Include(x => x.VideoTags)
                .SingleAsync();

        var validTags = request.Tags.Except(video.VideoTags.Select(t => t.Name))
            .ToList();

        foreach (var tag in validTags)
        {
            video.AddTag(tag);                        
        }

        await DbContext.CommitChangesAsync(cancellationToken);

        return new AddTagsToVideoResponse(request.VideoId, validTags.Count);
    }
}
