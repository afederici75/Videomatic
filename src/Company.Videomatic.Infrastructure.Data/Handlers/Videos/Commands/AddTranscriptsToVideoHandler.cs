namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public class AddTranscriptsToVideoHandler : BaseRequestHandler<AddTranscriptsToVideoCommand, AddTranscriptsToVideoResponse>
{
    public AddTranscriptsToVideoHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<AddTranscriptsToVideoResponse> Handle(AddTranscriptsToVideoCommand request, CancellationToken cancellationToken = default)
    {
        var currentUserVideoTranscripts = await DbContext.Transcripts
                .Where(x => x.VideoId == request.VideoId)
                .ToListAsync(cancellationToken);

        return new AddTranscriptsToVideoResponse(-1, new Dictionary<string, long>());
    }
}
