using Company.Videomatic.Application.Features.Model;
using System.Diagnostics;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public class AssignVideoThumnbailsHandler : BaseRequestHandler<AssignVideoThumnbailsCommand, AssignVideoThumnbailsResponse>
{
    public AssignVideoThumnbailsHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<AssignVideoThumnbailsResponse> Handle(AssignVideoThumnbailsCommand request, CancellationToken cancellationToken)
    {
        var video = await DbContext.Videos
            .Where(x => x.Id == request.VideoId)
            .SingleAsync(cancellationToken);

        var processed = new HashSet<ThumbnailResolutionDTO>();
        foreach (var thumb in request.Thumbnails)
        {
            //Thumbnail? item = video.Thumbnails.SingleOrDefault(x => (int)x.Resolution == (int)thumb.Resolution);
            //
            //if (item is null)
            //{
                video.AddThumbnail(thumb.Location, (Domain.Videos.ThumbnailResolution)thumb.Resolution, thumb.Height, thumb.Width);
            //}
            //else
            //{
            //    Mapper.Map<ThumbnailInfo, Thumbnail>(thumb, item);                
            //}
        
            processed.Add(thumb.Resolution);
        }

        await DbContext.CommitChangesAsync(cancellationToken);
        
        return new AssignVideoThumnbailsResponse(request.VideoId, processed.ToArray());
    }    
}
