using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public class SetVideoTagsHandler : BaseRequestHandler<SetVideoTags, SetVideoTagsResponse>
{
    public SetVideoTagsHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<SetVideoTagsResponse> Handle(SetVideoTags request, CancellationToken cancellationToken = default)
    {
        var video = await DbContext.Videos
                .Where(x => x.Id == request.VideoId)                
                .Include(x => x.Tags)
                .SingleAsync();

        var validTags = request.Tags.Except(video.Tags.Select(t => t.Name))
            .ToArray();

        video.AddTags(validTags);                        
        
        await DbContext.CommitChangesAsync(cancellationToken);

        return new SetVideoTagsResponse(request.VideoId, validTags.Length);
    }
}
