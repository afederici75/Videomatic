using Company.Videomatic.Application.Features.Transcript.Commands;
using Company.Videomatic.Domain.Aggregates.Transcript;
using Company.Videomatic.Domain.Specifications;
using Company.Videomatic.Domain.Specifications.Transcripts;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public class UpdateTranscriptsHandler : IRequestHandler<UpdateTranscriptCommand, UpdateTranscriptResponse>
{
    private readonly IRepository<Transcript> _repository;
    private readonly IMapper _mapper;

    public UpdateTranscriptsHandler(IRepository<Transcript> repository,
                                    IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<UpdateTranscriptResponse> Handle(
        UpdateTranscriptCommand request, 
        CancellationToken cancellationToken)
    {
        var spec = new TranscriptByVideoIdsSpecification(request.VideoId);

        var transcript = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (transcript is null)
        {
            transcript = await _repository.AddAsync(_mapper.Map<Transcript>(request));
        }
        else
        {
            _mapper.Map(request, transcript);
        }   

        var cnt = await _repository.SaveChangesAsync(cancellationToken);
        
        return new UpdateTranscriptResponse(transcript.Id);
    }


    //public override async Task<AddTranscriptsToVideoResponse> Handle(
    //    AddTranscriptsToVideoCommand request, 
    //    CancellationToken cancellationToken)
    //{        
    //    throw new NotImplementedException();
    //
    //    //var video = await DbContext.Videos
    //    //      .Where(x => x.Id == request.VideoId)
    //    //      .Include(x => x.Transcripts)              
    //    //      .SingleAsync(cancellationToken);
    //    //
    //    //var processed = new Dictionary<string, TranscriptId>();
    //    //foreach (var trans in request.Transcripts)
    //    //{
    //    //    var transcript = video.Transcripts.FirstOrDefault(x => x.Language == trans.Language); // TODO: case sensitivity
    //    //    if (transcript == null)
    //    //    {
    //    //        //item = Mapper.Map<TranscriptPayload, Transcript>(trans);
    //    //        transcript = video.AddTranscript(trans.Language);
    //    //        foreach (var x in trans.Lines)
    //    //        {
    //    //            transcript.AddLine(x.Text, x.Duration, x.StartsAt);
    //    //        }
    //    //    }
    //    //    else
    //    //    {
    //    //        Mapper.Map<TranscriptPayload, Transcript>(trans, transcript);
    //    //    }
    //    //
    //    //    processed.Add(transcript.Language, transcript.Id);
    //    //}
    //    //
    //    //
    //    //await DbContext.CommitChangesAsync(cancellationToken);
    //    //
    //    //return new AddTranscriptsToVideoResponse(request.VideoId, processed);
    //}

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
