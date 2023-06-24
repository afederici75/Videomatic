using Company.Videomatic.Application.Features.Model;
using System.Diagnostics;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public class AddThumbnailsToVideoHandler : BaseRequestHandler<AddThumnbailsToVideoCommand, AddThumbnailsToVideoResponse>
{
    public AddThumbnailsToVideoHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<AddThumbnailsToVideoResponse> Handle(AddThumnbailsToVideoCommand request, CancellationToken cancellationToken)
    {
        var video = await DbContext.Videos
            .Where(x => x.Id == request.VideoId)
            .Include(x => x.Thumbnails)            
            .SingleAsync(cancellationToken);

        var processed = new Dictionary<ThumbnailResolution, long>();
        foreach (var thumb in request.Thumbnails)
        {
            var item = video.Thumbnails.FirstOrDefault(x => x.Resolution == thumb.Resolution);
            if (item == null)
            {
                item = Mapper.Map<ThumbnailPayload, Thumbnail>(thumb);
                video.AddThumbnail(item);                
            }
            else
            {
                Mapper.Map<ThumbnailPayload, Thumbnail>(thumb, item);                
            }

            processed.Add(item.Resolution, item.Id);
        }


        await DbContext.CommitChangesAsync(cancellationToken);

        return new AddThumbnailsToVideoResponse(request.VideoId, processed);
    }

    public async Task<AddThumbnailsToVideoResponse> Handle2(AddThumnbailsToVideoCommand request, CancellationToken cancellationToken = default)
    {
        var currentVideoThumbnails = await DbContext.Thumbnails
               .Where(x => x.VideoId == request.VideoId)
               .ToListAsync(cancellationToken);

        var processed = new Dictionary<ThumbnailResolution, long>();
        foreach (var thumb in request.Thumbnails)
        { 
            var item = currentVideoThumbnails.FirstOrDefault(x => x.Resolution == thumb.Resolution);
            if (item == null)
            {
                item = Mapper.Map<ThumbnailPayload, Thumbnail>(thumb);
                //item.VideoId = request.VideoId;

                DbContext.Add(item);                
            }
            else
            { 
                Mapper.Map<ThumbnailPayload, Thumbnail>(thumb, item);
                DbContext.Update(item);                
            }

            processed.Add(item.Resolution, item.Id);
        }

        
        await DbContext.CommitChangesAsync(cancellationToken);

        return new AddThumbnailsToVideoResponse(request.VideoId, processed);
    }
}
