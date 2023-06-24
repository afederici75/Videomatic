using Company.Videomatic.Application.Features.Model;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public class AddTranscriptsToVideoHandler : BaseRequestHandler<AddTranscriptsToVideoCommand, AddTranscriptsToVideoResponse>
{
    public AddTranscriptsToVideoHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<AddTranscriptsToVideoResponse> Handle(AddTranscriptsToVideoCommand request, CancellationToken cancellationToken)
    {
        var video = await DbContext.Videos
              .Where(x => x.Id == request.VideoId)
              .Include(x => x.Transcripts)
              .SingleAsync(cancellationToken);

        var processed = new Dictionary<string, long>();
        foreach (var trans in request.Transcripts)
        {
            var item = video.Transcripts.FirstOrDefault(x => x.Language == trans.Language); // TODO: case sensitivity
            if (item == null)
            {
                item = Mapper.Map<TranscriptPayload, Transcript>(trans);
                video.AddTranscript(item);  
            }
            else
            {
                Mapper.Map<TranscriptPayload, Transcript>(trans, item);                
            }

            processed.Add(item.Language, item.Id);
        }


        await DbContext.CommitChangesAsync(cancellationToken);

        return new AddTranscriptsToVideoResponse(request.VideoId, processed);
    }

    //public async Task<AddTranscriptsToVideoResponse> Handle2(AddTranscriptsToVideoCommand request, CancellationToken cancellationToken = default)
    //{
    //    var currentTranscripts = await DbContext.Transcripts
    //          .Where(x => x.VideoId == request.VideoId)
    //          .ToListAsync(cancellationToken);
    //
    //    var processed = new Dictionary<string, long>();
    //    foreach (var trans in request.Transcripts)
    //    {
    //        var item = currentTranscripts.FirstOrDefault(x => x.Language == trans.Language); // TODO: case sensitivity
    //        if (item == null)
    //        {
    //            item = Mapper.Map<TranscriptPayload, Transcript>(trans);
    //            //item.VideoId = request.VideoId;
    //
    //            DbContext.Add(item);
    //        }
    //        else
    //        {
    //            Mapper.Map<TranscriptPayload, Transcript>(trans, item);
    //            DbContext.Update(item);
    //        }
    //
    //        processed.Add(item.Language, item.Id);
    //    }
    //
    //
    //    await DbContext.SaveChangesAsync(cancellationToken);
    //    
    //    return new AddTranscriptsToVideoResponse(request.VideoId, processed);
    //}
}
