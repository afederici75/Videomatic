namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public class AddArtifactsToVideoCommandHandler : BaseRequestHandler<AddArtifactToVideoCommand, AddArtifactToVideoResponse>
{
    public AddArtifactsToVideoCommandHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<AddArtifactToVideoResponse> Handle(AddArtifactToVideoCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //var video = await DbContext.Videos
        //      .Where(x => x.Id == request.VideoId)
        //      .Include(x => x.Transcripts)
        //      .SingleAsync(cancellationToken);
        //
        //var artifact = video.AddArtifact(request.Title, request.Type, request.Text);
        //
        //var res = await DbContext.CommitChangesAsync(cancellationToken);
        //
        //return new AddArtifactToVideoResponse(request.VideoId, artifact.Id);
    }
}