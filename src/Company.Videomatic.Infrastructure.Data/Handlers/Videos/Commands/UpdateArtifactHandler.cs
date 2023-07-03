using Company.Videomatic.Domain.Aggregates.Artifact;
using Company.Videomatic.Domain.Specifications;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public class UpdateArtifactHandler : IRequestHandler<UpdateArtifactCommand, UpdateArtifactResponse>
{
    private readonly IRepository<Artifact> _repository;
    private readonly IMapper _mapper;

    public UpdateArtifactHandler(IRepository<Artifact> repository,
                                 IMapper mapper) 
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<UpdateArtifactResponse> Handle(UpdateArtifactCommand request, CancellationToken cancellationToken)
    {
        var spec = new ArtifactByVideoIdsSpecification(request.VideoId);

        var artifact = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (artifact is null)
        {
            artifact = await _repository.AddAsync(_mapper.Map<Artifact>(request));
        }
        else
        {
            _mapper.Map(request, artifact);
        }

        var cnt = await _repository.SaveChangesAsync(cancellationToken);

        return new UpdateArtifactResponse(artifact.Id);
    }

    //public override async Task<UpdateArtifactResponse> Handle(UpdateArtifactCommand request, CancellationToken cancellationToken)
    //{
    //    throw new NotImplementedException();
    //    //var video = await DbContext.Videos
    //    //      .Where(x => x.Id == request.VideoId)
    //    //      .Include(x => x.Transcripts)
    //    //      .SingleAsync(cancellationToken);
    //    //
    //    //var artifact = video.AddArtifact(request.Title, request.Type, request.Text);
    //    //
    //    //var res = await DbContext.CommitChangesAsync(cancellationToken);
    //    //
    //    //return new AddArtifactToVideoResponse(request.VideoId, artifact.Id);
    //}
}